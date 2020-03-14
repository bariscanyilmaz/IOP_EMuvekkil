using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMuvekkil.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class MasrafController : Controller
    {
        private IMapper _mapper;
        private IMasrafRepository _masrafRepository;
        private IDavaRepository _davaRepository;
        private UserManager<ApplicationUser> _userManager;

        public MasrafController(IMapper mapper, IMasrafRepository masrafRepository,
        IDavaRepository davaRepository, UserManager<ApplicationUser> userManager)
        {
            _davaRepository = davaRepository;
            _masrafRepository = masrafRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMasraf([FromBody]MasrafViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.OwnerUserName);
                var dava = _davaRepository.GetDava(model.DavaId);
                var masraf = new Masraf { Owner = user, Amount = model.Amount, Dava = dava, Date = model.Date.Date, Description = model.Description };
                var newMasraf = _masrafRepository.AddMasraf(masraf);
                var marafvm = _mapper.Map<MasrafViewModel>(newMasraf);
                return Ok(marafvm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteMasraf([FromBody]MasrafViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.OwnerUserName);
                var masraf = _masrafRepository.GetMasraf(model.Id);
                if (masraf.Owner == user)
                {
                    _masrafRepository.DeleteMasraf(model.Id);
                    return Ok(model);
                }
                return BadRequest("Belirtilen masraf bu kullanıcı tarafından eklenmemiş.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("[action]")]
        public IActionResult GetMasrafsbyDavaId(int id)
        {
            try
            {
                var masrafs = _masrafRepository.GetMasrafs(id);
                var masrafVMs = _mapper.Map<IQueryable<Masraf>,IList<MasrafViewModel>>(masrafs);
                return Ok(masrafVMs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
          
            
    }
}
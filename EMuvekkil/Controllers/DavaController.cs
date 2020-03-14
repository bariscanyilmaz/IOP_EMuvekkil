using System;
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

    [Route("api/[controller]")]
    [Authorize]
    public class DavaController : Controller
    {
        private IDavaRepository _davaRepository;
        private IDavaStateRepository _davaStateRepository;

        private UserManager<ApplicationUser> _userManager;


        private IMapper _mapper;


        public DavaController(IDavaRepository davaRepository, UserManager<ApplicationUser> userManager,
        IMapper mapper, IDavaStateRepository davaStateRepository)
        {
            _davaRepository = davaRepository;
            _userManager = userManager;
            _mapper = mapper;
            _davaStateRepository = davaStateRepository;

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateDava([FromBody]DavaViewModel davaVm)
        {
            try
            {
                var dava = _davaRepository.GetDava(davaVm.Id);

                if (dava != null)
                {
                    var avukat = await _userManager.FindByIdAsync(davaVm.AvukatId);
                    var muvekkil = await _userManager.FindByIdAsync(davaVm.MuvekkilId);
                    var davaState = _davaStateRepository.GetDavaState(davaVm.DavaStateId);

                    dava.Name = davaVm.Name;
                    dava.Avukat = avukat;
                    dava.Muvekkil = muvekkil;
                    dava.DavaState = davaState;

                    _davaRepository.UpdateDava(dava);
                    return Ok(davaVm);
                }
                return BadRequest("Böyle bir dava bulunamadı");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDavas([FromQuery]string email, Role role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                List<Dava> davaList = new List<Dava>();

                switch (role)
                {
                    case Role.admin:
                        davaList = _davaRepository.GetDavas().ToList();
                        break;
                    case Role.avukat:
                        davaList = _davaRepository.GetDavas().Where(d => d.Avukat == user).ToList();
                        break;
                    case Role.muvekkil:
                        davaList = _davaRepository.GetDavas().Where(d => d.Muvekkil == user).ToList();
                        break;

                }

                var result = _mapper.Map<IList<DavaViewModel>>(davaList);

                return Ok(result);

            }
            return BadRequest("Böyle bir kullanıcı bulunamadğı için ilgili davalar getirilemedi");
        }

        [HttpGet("[action]")]
        public IActionResult GetDavasBy([FromQuery] string muvekkilId, string avukatId)
        {
            var davas = _davaRepository.GetDavas().Where(d => d.AvukatId == avukatId && d.MuvekkilId == muvekkilId);
            var result = _mapper.Map<IList<DavaViewModel>>(davas);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public IActionResult GetDavasByMuvekkil([FromQuery] string muvekkilId)
        {
            var davas = _davaRepository.GetDavas().Where(d => d.MuvekkilId == muvekkilId);
            var result = _mapper.Map<IList<DavaViewModel>>(davas);
            return Ok(result);
        }

        [HttpGet("[action]")]   
        public IActionResult GetDavasByAvukat([FromQuery] string avukatId)
        {
            var davas = _davaRepository.GetDavas().Where(d => d.AvukatId == avukatId);
            var result = _mapper.Map<IList<DavaViewModel>>(davas);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public IActionResult DeleteDava([FromBody]DavaViewModel davaVm)
        {
            try
            {
                _davaRepository.DeleteDava(davaVm.Id);
                return Ok(davaVm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("[action]")]
        public IActionResult GetDava(int id)
        {
            var dava = _davaRepository.GetDava(id);
            if (dava != null)
            {
                var davaVM = _mapper.Map<DavaViewModel>(dava);
                return Ok(davaVM);
            }
            return BadRequest("Ne yazık ki dava bulunamadı");

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDava([FromBody] DavaViewModel model)
        {
            try
            {
                var muvekkil = await _userManager.FindByIdAsync(model.MuvekkilId);
                var avukat = await _userManager.FindByIdAsync(model.AvukatId);
                var newDavaState = _davaStateRepository.GetNewDavaState();

                var dava = new Dava() { Avukat = avukat, Name = model.Name, Muvekkil = muvekkil, DavaState = newDavaState };
                _davaRepository.AddDava(dava);
                var davaVM = _mapper.Map<DavaViewModel>(dava);
                return Ok(davaVM);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpGet("[action]")]
        public IActionResult GetDavaStates()
        {
            var davaStates = _davaStateRepository.GetDavaStates();
            var davaStateVMs = _mapper.Map<IList<DavaState>>(davaStates);
            return Ok(davaStateVMs);
        }


    }
}


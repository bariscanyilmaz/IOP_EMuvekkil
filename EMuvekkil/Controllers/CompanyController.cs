using System.Collections.Generic;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMuvekkil.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController : Controller
    {

        private ICompanyRepository _companyRepository;
        private IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public IActionResult GetCompanies()
        {
            var companies = _companyRepository.GetCompanies();
            var compVMs = _mapper.Map<IList<CompanyViewModel>>(companies);
            return Ok(compVMs);
        }


        [HttpPost("[action]")]
        public IActionResult AddCompany([FromBody] CompanyViewModel model)
        {
            var comp = new Company { Name = model.Name };
            var newComp = _companyRepository.AddCompany(comp);
            var compVM = _mapper.Map<CompanyViewModel>(newComp);
            return Ok(compVM);
        }


        [HttpPost("[action]")]
        public IActionResult DeleteCompany([FromBody] CompanyViewModel model)
        {
            _companyRepository.DeleteCompany(model.Id);
            return Ok(model);
        }

    }

}
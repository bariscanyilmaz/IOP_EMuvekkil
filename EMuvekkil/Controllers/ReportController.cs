using System.Linq;
using System.Threading;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMuvekkil.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : Controller
    {
        private IDavaRepository _davaRepository;
        private IMasrafRepository _masrafRepository;
        private IMessageRepository _messageRepository;
        private IDocumentRepository _documentRepository;
        private IMapper _mapper;
        public ReportController(
            IDavaRepository davaRepository,
            IDocumentRepository documentRepository,
            IMessageRepository messageRepository,
            IMasrafRepository masrafRepository,
            IMapper mapper
            )
        {
            _davaRepository = davaRepository;
            _documentRepository = documentRepository;
            _masrafRepository = masrafRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public IActionResult GetList([FromQuery] ReportViewModel model)
        {

            var messages = _messageRepository.GetMessagesByDavaId(model.DavaId).ToList();
            var documents = _documentRepository.GetDocuments().Where(d=>d.DavaId==model.DavaId).ToList();
            var masrafs = _masrafRepository.GetMasrafs().Where(d=>d.DavaId==model.DavaId).ToList();
            
            if (model.DateDisabled)
            {
                messages.Where(m=> m.Date <= model.EndDate && m.Date >= model.StartDate).ToList();
                documents.Where(m=> m.Date <= model.EndDate && m.Date >= model.StartDate).ToList();
                masrafs.Where(m=> m.Date <= model.EndDate && m.Date >= model.StartDate).ToList();
            }

            var reportListModel = new ReportListModel() { Masrafs = masrafs, Documents = documents, Messages = messages };
            var result = _mapper.Map<ReportListViewModel>(reportListModel);
            return Ok(result);
        }   

        [HttpGet("[action]")]
        public IActionResult GetReport([FromQuery] ReportViewModel model)
        {
            return Ok();
        }


    }

}
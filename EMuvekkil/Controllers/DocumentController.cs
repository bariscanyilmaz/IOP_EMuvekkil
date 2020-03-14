
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMuvekkil.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentController : Controller
    {
        private IDocumentRepository _documentRepository;
        private UserManager<ApplicationUser> _userManager;
        private IDavaRepository _davaRepository;
        private IMapper _mapper;

        public DocumentController(IDocumentRepository documentRepository, UserManager<ApplicationUser> userManager, IDavaRepository davaRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _userManager = userManager;
            _davaRepository = davaRepository;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DowloadFile(int docId)
        {
            var document = _documentRepository.GetDocument(docId);
            if (document != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", document.FilePath);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/octet-stream");
            }
            return BadRequest("Döküman bulunamadı");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddDocument([FromForm]DocumentViewModel model, IFormFile file)
        {

            try
            {

                var user = await _userManager.FindByEmailAsync(model.OwnerUserName);
                var dava = _davaRepository.GetDava(model.DavaId);
                var document = new Document();
                if (user == null) { throw new Exception("Kullanıcı bulunamadı!"); }
                if (file == null) { throw new Exception("Dosya Bulunamadı!"); }
                if (dava == null) { throw new Exception("Dosya ile ilişkilendirilecek dava bulunamadı!"); }
                var filePath = model.DavaId.ToString() + "-" + model.Date.ToShortDateString() + "-" + Path.GetRandomFileName() + file.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", filePath);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream).Wait();
                }
                document.Date = model.Date;
                document.Description = model.Description;
                document.FileName = file.FileName;

                document.Dava = dava;
                document.Owner = user;
                document.FilePath = filePath;
                document.IsActive = true;

                var doc = _documentRepository.AddDocument(document);
                var docVM = _mapper.Map<DocumentViewModel>(doc);


                return Ok(docVM);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public IActionResult ChangeStatus([FromBody] DocumentViewModel model)
        {
            try
            {
                var doc = _documentRepository.GetDocument(model.Id);
                _documentRepository.ChangeDocumentStatus(doc);

                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult DeleteDocument(int documentId)
        {
            var doc = _documentRepository.GetDocument(documentId);
            if (doc != null)
            {
                _documentRepository.DeleteDocument(documentId);
                System.IO.File.Delete(doc.FilePath);
                return Ok();
            }
            return BadRequest("Döküman bulunamadı.");

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDocuments(string email)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(email);

                var docs = _documentRepository.GetDocuments().Where(d => d.Owner == user);
                var docsVM = _mapper.Map<IList<DocumentViewModel>>(docs);
                return Ok(docsVM);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("[action]")]
        public IActionResult GetDocumentsByDavaId(int id)
        {
            var docs = _documentRepository.GetDocuments().Where(d => d.DavaId == id);
            var docVMs = _mapper.Map<IList<DocumentViewModel>>(docs);
            return Ok(docVMs);
        }

    }

}

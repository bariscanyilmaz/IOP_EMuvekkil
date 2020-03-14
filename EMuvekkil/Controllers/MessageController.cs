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
    public class MessageController : Controller
    {
        private IMessageRepository _messageRepository;
        private IDavaRepository _davaRepository;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public MessageController(IMessageRepository messageRepository, UserManager<ApplicationUser> userManager, IDavaRepository davaRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
            _davaRepository = davaRepository;
            _mapper = mapper;
        }

       
        [HttpGet("[action]")]
        public IActionResult GetMessage(int id)
        {

            var message = _messageRepository.GetMessage(id);
            if (message != null)
            {
                var result = _mapper.Map<MessageViewModel>(_messageRepository.GetMessage(id));
                return Ok(result);
            }
            return BadRequest("Mesaj Bulunamadı");
        }

        [HttpPost("[action]")]
        public IActionResult DeleteMessage(int id)
        {
            _messageRepository.DeleteMessage(id);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMessage([FromBody]MessageViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.OwnerUserName);
            if (user != null)
            {
                var message=new Message();
                var dava = _davaRepository.GetDava(model.DavaId);
                if (dava != null)
                {
                    message.Dava = dava;
                    message.Owner = user;
                    message.Text=model.Text;
                    message.Date=model.Date.Date;
                    message.IsActive=true;
                    var mes= _messageRepository.AddMessage(message);
                    var mesVM= _mapper.Map<MessageViewModel>(mes);
                    return Ok(mesVM);
                }
                return BadRequest("Dava bulunmadığı için mesaj eklenemedi");
            }
            return BadRequest("Kullanıcı bulunmadığı için mesaj eklenemedi");
        }

        [HttpGet("[action]")]
        public IActionResult GetMessagesByDavaId(int id)
        {
            var messages = _messageRepository.GetMessagesByDavaId(id);
            var messagesVM = _mapper.Map<IList<MessageViewModel>>(messages);
            return Ok(messagesVM);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMessages(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var messages = _messageRepository.GetMessages().Where(m => m.Owner == user);
            var messagesVM = _mapper.Map<IList<MessageViewModel>>(messages);
            return Ok(messagesVM);
        }

        [HttpPost("[action]")]
        public IActionResult ChangeStatus([FromBody] MessageViewModel model)
        {

            try
            {
                var message = _messageRepository.GetMessage(model.Id);
                _messageRepository.ChangeMessageStatus(message);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

    }
}

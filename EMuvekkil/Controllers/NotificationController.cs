using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMuvekkil.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private IMapper _mapper;
        private INotificationService _notificationService;
        private UserManager<ApplicationUser> _userManager;
        public NotificationController(IMapper mapper,INotificationService notificationService,UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _notificationService=notificationService;
            _userManager=userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotifications([FromQuery]string id)
        {
            var user=await _userManager.FindByIdAsync(id);
            var notifications= _notificationService.GetNotificationsByUser(user).ToList();

            var notifvms= _mapper.Map<IList<NotificationViewModel>>(notifications);

            return Ok(notifvms);
        }

        [HttpPost("[action]")]
        public IActionResult MarkReaded([FromBody]NotificationViewModel model)
        {
            var notif= _notificationService.GetNotification(model.Id);
            _notificationService.MarkReaded(notif);
            return Ok(model);
        }

        
    }
}
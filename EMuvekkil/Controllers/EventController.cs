using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using EMuvekkil.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMuvekkil.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class EventController : Controller
    {
        private UserManager<ApplicationUser> _userManger;
        private IEventRepository _eventRepository;
        private IEventUsersRepository _eventUsersRepository;
        private IMapper _mapper;
        private IReminderService _reminderService;
        private IMailService _mailService;
        private INotificationService _notificationService;

        public EventController(
            IEventRepository eventRepository,
            UserManager<ApplicationUser> userManager,
            IEventUsersRepository eventUsersRepository,
            IMapper mapper,
            IReminderService reminderService,
            IMailService mailService,
            INotificationService notificationService
            )
        {
            _eventRepository = eventRepository;
            _userManger = userManager;
            _eventUsersRepository = eventUsersRepository;
            _mapper = mapper;
            _reminderService = reminderService;
            _mailService = mailService;
            _notificationService = notificationService;
        }

        [HttpGet("[action]")]
        public IActionResult GetEvents()
        {
            var events = _eventRepository.GetEvents().Select(d => new EventViewModel
            {
                Start = d.Date,
                Id = d.Id,
                RememberDate = d.RememberDate,
                Title = d.Title,
                Users = _mapper.Map<IList<UserViewModel>>(d.EventUsers.Select(v => v.User).ToList())
            });

            return Ok(events);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddEvent([FromBody]EventViewModel model)
        {
            var newEv = new Event() { Date = model.Start.ToLocalTime(), RememberDate = model.RememberDate.ToLocalTime(), Title = model.Title };

            newEv = _eventRepository.AddEvent(newEv);
            IList<ApplicationUser> users = new List<ApplicationUser>();

            foreach (var item in model.Users)
            {
                var user = await _userManger.FindByIdAsync(item.Id);
                var newEvUsrs = new EventUsers() { Event = newEv, User = user };
                users.Add(user);

                _eventUsersRepository.AddEventUsers(newEvUsrs);
            }

            foreach (var item in users)
            {
                _mailService.SendMail(to: item.Email, body: newEv.Title + " " + newEv.Date, subject: newEv.Title);
            }

            _notificationService.SendNotification(newEv.Title + " " + newEv.Date + "'da", users);
            var uservms = _mapper.Map<IList<UserViewModel>>(users);

            newEv.ReminderJobId = _reminderService.AddReminder(model, uservms);
            _eventRepository.UpdateEvent(newEv);

            var events = _eventRepository.GetEvents().Select(d => new EventViewModel
            {
                Start = d.Date,
                Id = d.Id,
                RememberDate = d.RememberDate,
                Title = d.Title,
                Users = _mapper.Map<IList<UserViewModel>>(d.EventUsers.Select(v => v.User).ToList())
            });

            return Ok(events);

        }


        [HttpPost("[action]")]
        public IActionResult DeleteEvent([FromBody]EventViewModel model)
        {

            var ev = _eventRepository.GetEvent(model.Id);
            _reminderService.DeleteReminder(ev.ReminderJobId);
            var events = _eventRepository.DeleteEvent(ev.Id).Select(v => new EventViewModel
            {
                Start = v.Date,
                Id = v.Id,
                RememberDate = v.RememberDate,
                Title = v.Title,
                ReminderJobId = v.ReminderJobId,
                Users = _mapper.Map<IList<UserViewModel>>(v.EventUsers.Select(f => f.User).ToList())
            });

            return Ok(events);
        }
    }
}
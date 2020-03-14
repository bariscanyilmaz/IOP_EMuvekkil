using System.Collections.Generic;
using System.Linq;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using EMuvekkil.Services.Abstract;

namespace EMuvekkil.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void SendNotification(string message, IList<ApplicationUser> users)
        {
            foreach (var item in users)
            {
                var model = new Notification() { Message = message, User = item, IsRead = false };
                _notificationRepository.AddNotification(model);
            }
        }

        public IQueryable<Notification> GetNotificationsByUser(ApplicationUser user)
        {
            return _notificationRepository.GetNotificationsByUser(user);
        }

        public void MarkReaded(Notification model)
        {
            _notificationRepository.UpdateNotification(model);
        }

        public Notification GetNotification(int id)
        {
            return _notificationRepository.GetNotification(id);
        }

        
    }

}
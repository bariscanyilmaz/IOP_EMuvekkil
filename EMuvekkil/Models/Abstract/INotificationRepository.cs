using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface INotificationRepository
    {
        Notification AddNotification(Notification model);
        Notification UpdateNotification(Notification model);
        void DeleteNotification(Notification model);
        IQueryable<Notification> GetNotificationsByUser(ApplicationUser user);
        Notification GetNotification(int id);
    }
}
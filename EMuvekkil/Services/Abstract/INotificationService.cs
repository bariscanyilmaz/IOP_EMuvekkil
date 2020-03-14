using System.Collections.Generic;
using System.Linq;
using EMuvekkil.Models;

namespace EMuvekkil.Services.Abstract
{
    public interface INotificationService
    {
        void SendNotification(string message, IList<ApplicationUser> users);

        IQueryable<Notification> GetNotificationsByUser(ApplicationUser user);
        void MarkReaded(Notification model);

        Notification GetNotification(int id);

    }
}
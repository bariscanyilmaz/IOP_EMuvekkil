using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{
    public class EfNotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EfNotificationRepository(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public Notification AddNotification(Notification model)
        {
            _dbContext.Notifications.Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public void DeleteNotification(Notification model)
        {
            _dbContext.Notifications.Remove(model);
            
        }

        public Notification UpdateNotification(Notification model)
        {
            var notification= _dbContext.Notifications.First(d=>d.Id==model.Id);
            if (notification!=null)
            {
                notification.IsRead=model.IsRead;
                notification.Message=model.Message;
                notification.User=model.User;
            }
            _dbContext.SaveChanges();
            return model;
        }

        public IQueryable<Notification> GetNotificationsByUser(ApplicationUser user)
        {
            return _dbContext.Notifications.Where(d=>d.User==user);
        }

        public Notification GetNotification(int id)
        {
            return _dbContext.Notifications.Where(d=>d.Id==id).Include(d=>d.User).First();
        }
    }

}
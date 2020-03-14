using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{
    public class EfEventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EfEventRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Event AddEvent(Event model)
        {
            _dbContext.Events.Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public IQueryable<Event> DeleteEvent(int eventId)
        {

            var ev = _dbContext.Events.FirstOrDefault(d => d.Id == eventId);
            _dbContext.Events.Remove(ev);
            _dbContext.SaveChanges();

            return _dbContext.Events;
        }

        public IQueryable<Event> GetEvents()
        {
            return _dbContext.Events.Include(d => d.EventUsers);
        }

        public Event GetEvent(int eventId)
        {
            return _dbContext.Events.Where(d => d.Id == eventId).Include(d => d.EventUsers).FirstOrDefault();
        }

        public Event UpdateEvent(Event model)
        {
            var job = _dbContext.Events.FirstOrDefault(d => d.Id == model.Id);
            if (job != null)
            {
                job.ReminderJobId = model.ReminderJobId;
                _dbContext.SaveChanges();
                return model;
            }
            throw new System.Exception("Event Not Found");
        }
    }
}
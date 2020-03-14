using System.Linq;
using EMuvekkil.Models.Abstract;

namespace EMuvekkil.Models.Concrete
{
    public class EfEventUsersRepository : IEventUsersRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public EfEventUsersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEventUsers(EventUsers model)
        {
            _dbContext.EventUsers.Add(model);
            _dbContext.SaveChanges();
        }

        public IQueryable<EventUsers> GetEventUsers(Event model)
        {
            return _dbContext.EventUsers.Where(m=>m.Event==model);
        }


    }
}
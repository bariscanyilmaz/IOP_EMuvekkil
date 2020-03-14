using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface IEventUsersRepository
    {
        void AddEventUsers(EventUsers model);

        IQueryable<EventUsers> GetEventUsers(Event model);
        
        
    }

}
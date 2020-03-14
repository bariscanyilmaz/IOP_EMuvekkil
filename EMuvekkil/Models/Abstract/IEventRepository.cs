using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface IEventRepository
    {
        IQueryable<Event> GetEvents();
        IQueryable<Event> DeleteEvent(int eventId);

        Event AddEvent(Event model);

        Event GetEvent(int eventId);
        Event UpdateEvent(Event model);
        
    }
}
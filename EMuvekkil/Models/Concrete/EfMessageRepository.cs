using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{
    public class EfMessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EfMessageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Message AddMessage(Message message)
        {
            message.IsActive = true;
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
            return message;
        }

        public void DeleteMessage(int id)
        {
            var message = _dbContext.Messages.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                _dbContext.Messages.Remove(message);
                _dbContext.SaveChanges();
            }
        }

        public Message GetMessage(int id)
        {
            return _dbContext.Messages.Where(m => m.Id == id).Include(m => m.Dava).First();
        }

        public IQueryable<Message> GetMessages()
        {
            return _dbContext.Messages.Include(m => m.Dava);
        }

        public IQueryable<Message> GetMessagesByDavaId(int davaId)
        {
            return _dbContext.Messages.Where(m => m.DavaId == davaId).Include(m => m.Dava).Include(m => m.Owner);
        }

        public void ChangeMessageStatus(Message message)
        {
            var mess = _dbContext.Messages.SingleOrDefault(m => m.Id == message.Id);
            if (mess != null)
            {
                mess.IsActive = !message.IsActive;
                _dbContext.SaveChanges();
            }
        }

    }
}
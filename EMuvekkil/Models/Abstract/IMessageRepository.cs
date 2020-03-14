using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface IMessageRepository
    {
        Message GetMessage(int id);
        void DeleteMessage(int id);
        Message AddMessage(Message message);
        IQueryable<Message> GetMessages();
        IQueryable<Message> GetMessagesByDavaId(int davaId);

        void ChangeMessageStatus(Message message);
        
    }
}
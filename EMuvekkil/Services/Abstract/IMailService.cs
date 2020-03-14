using EMuvekkil.Models;

namespace EMuvekkil.Services.Abstract
{
    public interface IMailService
    {
        void SendMail(string to,string body,string subject);

    }
}
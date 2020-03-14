using System.Net.Mail;
using EMuvekkil.Models;
using EMuvekkil.Services.Abstract;
using Microsoft.Extensions.Options;

namespace EMuvekkil.Services.Concrete
{
    public class MailService : IMailService
    {
        private readonly EmailSettings _emailSettings;
        
        public MailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings=emailSettings.Value;
        }
        public void SendMail(string to, string body, string subject)
        {
            try
            {
                var mailMessage=new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From=new MailAddress(_emailSettings.Sender);
                mailMessage.Body=body;
                mailMessage.Subject=subject;
                
                using (SmtpClient client=new SmtpClient())
                {
                    client.Host=_emailSettings.MailServer;
                    client.Port=_emailSettings.MailPort;
                    client.Credentials=new System.Net.NetworkCredential(_emailSettings.Sender,_emailSettings.Password);
                    client.EnableSsl=false;
                    client.Send(mailMessage);
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
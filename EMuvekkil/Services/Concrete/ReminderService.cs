using System;
using System.Collections.Generic;
using System.Net.Mail;
using EMuvekkil.Models;
using EMuvekkil.Services.Abstract;
using Hangfire;
using Microsoft.Extensions.Options;

namespace EMuvekkil.Services.Concrete
{
    public class ReminderService : IReminderService
    {
        private readonly EmailSettings _emailSettings;
        public ReminderService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public string AddReminder(EventViewModel ev, IList<UserViewModel> users)
        {
            return BackgroundJob.Schedule(() => SendRemindMail(ev, users), ev.RememberDate);
        }

        public bool DeleteReminder(string jobId)
        {
            return BackgroundJob.Delete(jobId);
        }

        public void SendRemindMail(EventViewModel ev, IList<UserViewModel> users)
        {
            foreach (var item in users)
            {
                try
                {
                    var mailMessage = new MailMessage();
                    mailMessage.To.Add(item.Email);
                    mailMessage.From = new MailAddress(_emailSettings.Sender);
                    mailMessage.Body = "Hatırlatma";
                    mailMessage.Subject =ev.Title+" " + ev.Start;

                    using (SmtpClient client = new SmtpClient())
                    {   
                        client.Host = _emailSettings.MailServer;
                        client.Port = _emailSettings.MailPort;
                        client.Credentials = new System.Net.NetworkCredential(_emailSettings.Sender, _emailSettings.Password);
                        client.EnableSsl = false;
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
}
using System.Net.Mail;
using Renterator.Services.Interfaces;

namespace Renterator.Services.AppServices.Security
{
    public class EmailService : IEmailService
    {
        public void Send(MailMessage message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}

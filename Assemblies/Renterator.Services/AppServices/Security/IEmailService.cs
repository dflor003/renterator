using System.Net.Mail;

namespace Renterator.Services.AppServices.Security
{
    public interface IEmailService
    {
        void Send(MailMessage message);
    }
}
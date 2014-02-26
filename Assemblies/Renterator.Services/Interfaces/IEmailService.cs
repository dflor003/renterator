using System.Net.Mail;

namespace Renterator.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(MailMessage message);
    }
}
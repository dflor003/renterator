using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;

namespace Renterator.Services.AppServices.Security
{
    public interface IPasswordRecoveryService
    {
        Result SendForgotPasswordEmail(string email);
        Result<PasswordResetInfo> IsValidPasswordResetToken(string token);
        Result ResetPassword(string tokenString, string userEmail, string newPassword);
    }
}
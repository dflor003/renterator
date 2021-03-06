using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;

namespace Renterator.Services.Interfaces
{
    public interface IPasswordRecoveryService
    {
        Result SendForgotPasswordEmail(string email);
        Result<PasswordResetInfo> GetPasswordResetInfo(string token);
        Result ResetPassword(string tokenString, string userEmail, string newPassword);
    }
}
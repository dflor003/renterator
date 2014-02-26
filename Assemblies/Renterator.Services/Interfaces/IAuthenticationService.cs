using System;
using Renterator.DataAccess.Model;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;

namespace Renterator.Services.Interfaces
{
    public interface IAuthenticationService : IDisposable
    {
        Result CreateAccount(UserAccountCreationInfo userInfo);
        Result Login(LoginInfo loginInfo);
        Result CreateAccount(UserAccountCreationInfo userInfo, out User user);
    }
}
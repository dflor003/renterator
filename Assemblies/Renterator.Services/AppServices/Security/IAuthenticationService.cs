using System;
using Renterator.DataAccess.Model;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;

namespace Renterator.Services.AppServices.Security
{
    public interface IAuthenticationService : IDisposable
    {
        Result CreateAccount(UserInfo userInfo);
        Result Login(LoginInfo loginInfo);
        Result CreateAccount(UserInfo userInfo, out User user);
    }
}
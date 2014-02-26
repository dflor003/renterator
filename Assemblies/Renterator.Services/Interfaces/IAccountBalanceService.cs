using System;
using Renterator.Services.Dto;

namespace Renterator.Services.Interfaces
{
    public interface IAccountBalanceService : IDisposable
    {
        AccountBalanceView GetAccountBalanceView(int userId);
    }
}
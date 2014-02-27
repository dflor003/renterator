using System;
using Renterator.Services.Dto;

namespace Renterator.Services.Interfaces
{
    public interface IBillManagementService : IDisposable
    {
        AccountBalanceView GetAccountBalanceView(int userId);
        BillsView GetBillsView();
    }
}
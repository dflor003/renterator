using System.Linq;
using Renterator.DataAccess.Infrastructure;
using Renterator.Services.Dto;
using Renterator.Services.Interfaces;

namespace Renterator.Services.AppServices.Business
{
    internal class BillManagementService : IBillManagementService
    {
        private readonly IDataAccessor dataAccessor;

        public BillManagementService(IDataAccessor dataAccessor)
        {
            this.dataAccessor = dataAccessor;
        }

        public AccountBalanceView GetAccountBalanceView(int userId)
        {
            AccountBalanceView result =
                (from account in dataAccessor.Accounts.Include(x => x.AccountEntries)
                 where account.UserId == userId
                 select new AccountBalanceView
                 {
                     Balance = account.AccountEntries.Sum(x => x.Amount),
                     RunningTotalBase = account.AccountEntries
                        .OrderByDescending(x => x.Date)
                        .ThenBy(x => x.Amount)
                        .Skip(10)
                        .Sum(x => x.Amount),
                     MostRecentEntries =
                        from e in account.AccountEntries
                            .OrderByDescending(x => x.Date)
                            .ThenBy(x => x.Amount)
                            .Take(10)
                        select new AccountEntryDto
                        {
                            Id = e.Id,
                            Amount = e.Amount,
                            Date = e.Date,
                            Description = e.Description
                        }

                 })
                 .FirstOrDefault();

            return result ?? new AccountBalanceView();
        }

        public void Dispose()
        {
            this.dataAccessor.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;

namespace Renterator.Services.Dto
{
    public class AccountBalanceView
    {
        public decimal Balance { get; set; }

        public decimal RunningTotalBase { get; set; }

        public int PastDueCount { get; set; }

        public IEnumerable<AccountEntryDto> MostRecentEntries { get; set; }
    }

    public class AccountEntryDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}

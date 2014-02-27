using System;

namespace Renterator.Services.Dto
{
    public class BillsView
    {
        public BillDto[] Bills { get; set; }
        public BillTypeDto[] BillTypes { get; set; }
    }

    public class BillDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public int BillTypeId { get; set; }
    }

    public class BillTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
using System;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreBill
    {
        public int BillId { get; set; }
        public string BillName { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsLate { get; set; }

        public int UserId { get; set; }
        public CoreUser User { get; set; }
    }
}

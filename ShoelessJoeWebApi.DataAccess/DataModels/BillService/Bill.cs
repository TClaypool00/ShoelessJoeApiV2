using System;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels.BillService
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public string BillName { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; } = false;
        public bool IsLate { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

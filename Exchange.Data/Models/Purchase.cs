using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.Data.Models
{
    [Table("Purchases")]
    public class Purchase
    {
        [Key]
        public int Id { get;set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
        public decimal AmountResult { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}

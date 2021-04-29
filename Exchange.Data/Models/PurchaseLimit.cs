using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.Data.Models
{
    [Table("PurchaseLimit")]
    public class PurchaseLimit
    {
        [Key]
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal AmountLimit { get; set; }
    }
}

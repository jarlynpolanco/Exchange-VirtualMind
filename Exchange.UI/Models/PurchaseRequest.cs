using System.ComponentModel.DataAnnotations;

namespace Exchange.UI.Models
{
    public class PurchaseRequest
    {
        [Required]
        [Range(0.1, float.MaxValue, ErrorMessage = "Please enter valid amount number")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Please enter valid ISO Currency")]
        [MinLength(3, ErrorMessage = "Please enter valid ISO Currency")]
        public string Currency { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid UserId")]
        public int UserId { get; set; }
    }

    public class PurchaseResponse 
    {
        public decimal AmountResult { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Exchange.Models.DTOs
{
    public class PurchaseDTO
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class PurchaseResponseDTO 
    {
        public decimal AmountResult { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

    }
}

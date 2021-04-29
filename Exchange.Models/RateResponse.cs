namespace Exchange.Models
{

    public class RateResponse
    {
        public decimal Buy { get; set; }

        public decimal Sale { get; set; }

        public string Currency { get; set; }

        public string DateUpdate { get; set; }
    }

}

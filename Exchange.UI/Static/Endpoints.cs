namespace Exchange.UI.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:5001/";
        public static string ExchangeRateEndpoint = $"{BaseUrl}api/ExchangeRate/Rate/";
        public static string ExchangePurchaseEndpoint = $"{BaseUrl}api/ExchangeRate/Purchase/";
    }
}

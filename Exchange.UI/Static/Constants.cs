namespace Exchange.UI.Static
{
    public static class Constants
    {
        public static string BaseUrl = "https://localhost:5001/";
        public static string ExchangeRateEndpoint = $"{BaseUrl}api/ExchangeRate/Rate/";
        public static string ExchangePurchaseEndpoint = $"{BaseUrl}api/ExchangeRate/Purchase/";
        public static string AllUsersEndpoint = $"{BaseUrl}api/User/AllUsers/";
        public static string[] Currencies = { "USD", "BRL" };
    }
}

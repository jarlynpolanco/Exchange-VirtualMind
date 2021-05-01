using Exchange.Contracts;
using Exchange.Models;
using System.Threading.Tasks;

namespace Exchange.Core.ExchangeRate.Currencies
{
    public class BRLRateProvider : ICurrencyRateProvider
    {
        private readonly ICurrencyExchange _currencyExchange;

        public BRLRateProvider(ICurrencyExchange currencyExchange)
        {
            _currencyExchange = currencyExchange;
        }

        public async Task<CurrencyRate> GetRateAsync(string currency)
        {
            var response = await _currencyExchange.Get();
            response.Buy /= 4;
            response.Sale /= 4;
            return response;
        }
    }
}

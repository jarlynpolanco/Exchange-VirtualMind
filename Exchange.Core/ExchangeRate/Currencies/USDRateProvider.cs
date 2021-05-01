using Exchange.Contracts;
using Exchange.Models;
using System.Threading.Tasks;

namespace Exchange.Core.ExchangeRate.Currencies
{
    public class USDRateProvider : ICurrencyRateProvider
    {
        private readonly ICurrencyExchange _currencyExchange;

        public USDRateProvider(ICurrencyExchange currencyExchange)
        {
            _currencyExchange = currencyExchange;
        }

        public async Task<CurrencyRate> GetRateAsync(string currency) => await _currencyExchange.Get();
    }
}

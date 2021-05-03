using Exchange.Contracts;
using Exchange.Core.Exceptions;
using Exchange.Core.ExchangeRate;
using Exchange.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Exchange.Core
{
    public class ExchangeRateProvider : IExchangeRateProvider
    {
        private readonly ExchangeRateProviderConfig _providerConfig;
        private readonly IServiceProvider _serviceProvider;

        public ExchangeRateProvider(IOptions<ExchangeRateProviderConfig> providerConfig, IServiceProvider serviceProvider)
        {
            _providerConfig = providerConfig.Value;
            _serviceProvider = serviceProvider;
        }

        public async Task<CurrencyRate> GetRateAsync(string currency)
        {
            var providerType = _providerConfig.GetProviderType(currency);

            if (!(_serviceProvider.GetService(providerType) is ICurrencyRateProvider provider))
            {
                throw new HttpStatusException($"Currency provider does not exist", HttpStatusCode.Forbidden);
            }

            return await provider.GetRateAsync(currency);
        }
    }
}

using Exchange.Contracts;
using Exchange.Core.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Exchange.Core.ExchangeRate
{
    public class ExchangeRateProviderConfig
    {
        private readonly IDictionary<string, Type> _providers;

        public ExchangeRateProviderConfig()
        {
            _providers = new Dictionary<string, Type>();
        }

        public void RegisterProvider<T>(string currency) where T : ICurrencyRateProvider
        {
            _providers.TryAdd(currency, typeof(T));
        }

        public Type GetProviderType(string currency)
        {
            if (!_providers.TryGetValue(currency, out Type providerType))
            {
                throw new HttpStatusException($"Currency not supported", HttpStatusCode.Forbidden);
            }

            return providerType;
        }
    }
}

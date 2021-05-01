using Exchange.Contracts;
using Exchange.Core;
using Exchange.Core.ExchangeRate;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExchangeRateProviderExtension
    {
        public static IServiceCollection AddExchangeRate<T>(this IServiceCollection services, string currency) where T : class, ICurrencyRateProvider
        {
            services.TryAddSingleton<IExchangeRateProvider, ExchangeRateProvider>();
            services.AddTransient<T>();
            services.Configure<ExchangeRateProviderConfig>(config => {
                config.RegisterProvider<T>(currency);
            });

            return services;
        }
    }
}

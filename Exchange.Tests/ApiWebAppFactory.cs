using Exchange.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.Tests
{
    public class ApiWebAppFactory : WebApplicationFactory<Exchange.Api.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => 
            {
                services.AddTransient<ICurrencyExchange, ProvinciaCurrencyExchangeFake>();
            });
        }
    }
}

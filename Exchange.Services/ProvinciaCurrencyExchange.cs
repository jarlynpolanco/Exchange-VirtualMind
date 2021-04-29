using Exchange.Contracts;
using Exchange.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Services
{
    public class ProvinciaCurrencyExchange : ICurrencyExchange
    {
        private readonly AppSettings _appSettings;
        private readonly HttpService _httpService;
        public ProvinciaCurrencyExchange(HttpService httpService, IOptions<AppSettings> appSettings)
        {
            _httpService = httpService;
            _appSettings = appSettings.Value;
        }

        public async Task<List<RateResponse>> Get()
        {
            var response = await _httpService.Get<string[]>(_appSettings.ExchangeRateService);
            return new List<RateResponse>()
            {
                new RateResponse()
                {
                    Buy = decimal.Parse(response[0]),
                    Sale = decimal.Parse(response[1]),
                    Currency = "USD",
                    DateUpdate = response[2]
                },
                new RateResponse()
                {
                    Buy = decimal.Parse(response[0])/4,
                    Sale = decimal.Parse(response[1])/4,
                    Currency = "BRL",
                    DateUpdate = response[2]
                },
            };
        }
    }
}

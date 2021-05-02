using Exchange.Models;
using Exchange.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.Tests
{
    public class ControllersTest : IClassFixture<ApiWebAppFactory>
    {
        private readonly ApiWebAppFactory _factory;
        private readonly HttpClient _client;

        public ControllersTest(ApiWebAppFactory factory) 
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_RateUSDSuccess() 
        {
            var response = await _client.GetAsync("api/ExchangeRate/USD");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStreamAsync();
            var currencyRate = await JsonSerializer.DeserializeAsync<GenericResponse<RateDTO>>(json);
            var rate = currencyRate.Data;

            Assert.Equal(89, rate.Buy);
        }



    }
}

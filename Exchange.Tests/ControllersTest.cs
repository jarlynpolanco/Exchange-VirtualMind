using Exchange.Models;
using Exchange.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.Tests
{
    public class ControllersTest : IClassFixture<ApiWebAppFactory>
    {
        private readonly HttpClient _client;

        public ControllersTest(ApiWebAppFactory factory) 
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Rate_USD_Success() 
        {
            var response = await _client.GetAsync("api/ExchangeRate/Rate/USD");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var currencyRate = JsonConvert.DeserializeObject<GenericResponse<RateDTO>>(json);
            var rate = currencyRate.Data;

            Assert.Equal(91.22M, rate.Buy); 
            Assert.Equal(90.10M, rate.Sale);
        }

        [Fact]
        public async Task Rate_BRL_Success()
        {
            var response = await _client.GetAsync("api/ExchangeRate/Rate/BRL");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var currencyRate = JsonConvert.DeserializeObject<GenericResponse<RateDTO>>(json);
            var rate = currencyRate.Data;

            Assert.Equal(22.805M, rate.Buy);
            Assert.Equal(22.525M, rate.Sale);
        }

        [Fact]
        public async Task Rate_Failure()
        {
            var response = await _client.GetAsync("api/ExchangeRate/Rate/DOP");
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Purchase_USD_Success()
        {
            decimal amount = 112M;
            var model = JsonConvert.SerializeObject(new PurchaseDTO()
            {
                Amount = amount,
                Currency = "USD",
                UserId = 5
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "api/ExchangeRate/Purchase")
            {
                Content = new StringContent(model,
                Encoding.UTF8, "application/json")
            };

            var httpResponsePurchase = await _client.SendAsync(request);
            httpResponsePurchase.EnsureSuccessStatusCode();
            var json = await httpResponsePurchase.Content.ReadAsStringAsync();
            var purchaseDTO = JsonConvert.DeserializeObject<GenericResponse<PurchaseResponseDTO>>(json);

            var responseRate = await _client.GetAsync("api/ExchangeRate/Rate/USD");
            responseRate.EnsureSuccessStatusCode();
            var jsonRate = await responseRate.Content.ReadAsStringAsync();
            var currencyRate = JsonConvert.DeserializeObject<GenericResponse<RateDTO>>(jsonRate);

            var resultValue = amount / currencyRate.Data.Buy;

            Assert.Equal(Math.Round(purchaseDTO.Data.AmountResult), Math.Round(resultValue));
        }

        [Fact]
        public async Task Purchase_BRL_Success()
        {
            decimal amount = 118M;
            var model = JsonConvert.SerializeObject(new PurchaseDTO()
            {
                Amount = amount,
                Currency = "BRL",
                UserId = 5
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "api/ExchangeRate/Purchase")
            {
                Content = new StringContent(model,
                Encoding.UTF8, "application/json")
            };

            var httpResponsePurchase = await _client.SendAsync(request);
            httpResponsePurchase.EnsureSuccessStatusCode();
            var json = await httpResponsePurchase.Content.ReadAsStringAsync();
            var purchaseDTO = JsonConvert.DeserializeObject<GenericResponse<PurchaseResponseDTO>>(json);

            var responseRate = await _client.GetAsync("api/ExchangeRate/Rate/BRL");
            responseRate.EnsureSuccessStatusCode();
            var jsonRate = await responseRate.Content.ReadAsStringAsync();
            var currencyRate = JsonConvert.DeserializeObject<GenericResponse<RateDTO>>(jsonRate);

            var resultValue = amount / currencyRate.Data.Buy;

            Assert.Equal(Math.Round(purchaseDTO.Data.AmountResult), Math.Round(resultValue));
        }

        [Fact]
        public async Task Purchase_Failure()
        {
            decimal amount = 100000000M;
            var model = JsonConvert.SerializeObject(new PurchaseDTO()
            {
                Amount = amount,
                Currency = "USD",
                UserId = 1
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "api/ExchangeRate/Purchase")
            {
                Content = new StringContent(model,
                Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);
            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}

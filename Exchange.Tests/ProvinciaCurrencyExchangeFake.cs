using Exchange.Contracts;
using Exchange.Models;
using System;
using System.Threading.Tasks;

namespace Exchange.Tests
{
    public class ProvinciaCurrencyExchangeFake : ICurrencyExchange
    {
        public Task<CurrencyRate> Get()
        {
            return Task.FromResult(new CurrencyRate()
            {
              Buy = 91.22M,
              Sale = 90.10M,
              DateUpdate = DateTime.Now.ToString()
            });
        }
    }
}
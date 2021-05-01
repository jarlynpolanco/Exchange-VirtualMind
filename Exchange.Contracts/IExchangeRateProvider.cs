using Exchange.Models;
using System.Threading.Tasks;

namespace Exchange.Contracts
{
    public interface IExchangeRateProvider
    {
        Task<CurrencyRate> GetRateAsync(string currency);
    }
}
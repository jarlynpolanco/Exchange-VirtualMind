using Exchange.Models;
using System.Threading.Tasks;

namespace Exchange.Contracts
{
    public interface ICurrencyRateProvider
    {
        Task<CurrencyRate> GetRateAsync(string currency);
    }
}

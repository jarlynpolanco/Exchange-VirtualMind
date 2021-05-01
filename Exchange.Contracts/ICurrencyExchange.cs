
using Exchange.Models;
using System.Threading.Tasks;

namespace Exchange.Contracts
{
    public interface ICurrencyExchange
    {
        Task<CurrencyRate> Get();
    }
}

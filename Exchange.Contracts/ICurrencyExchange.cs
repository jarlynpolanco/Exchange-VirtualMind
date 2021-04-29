
using Exchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Contracts
{
    public interface ICurrencyExchange
    {
        Task<List<RateResponse>> Get();
    }
}

using System.Threading.Tasks;

namespace Exchange.Contracts
{
    public interface IBaseServiceRepository<T> where T : class
    {
        Task<T> Get(string url);
    }
}

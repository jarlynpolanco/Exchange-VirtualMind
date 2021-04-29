using Exchange.Contracts;
using Exchange.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exchange.Services
{
    public class BaseServiceRepository<T> : IBaseServiceRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;
        private readonly ILog _log;

        public BaseServiceRepository(IHttpClientFactory client, ILog log) 
        {
            _client = client;
            _log = log;
        }

        public async Task<T> Get(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{url}");

                var client = _client.CreateClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception e) 
            {
                _log.WriteLog(e.Message, LogTypeEnum.ERROR.ToString());
            }

            return null;
        }
    }
}

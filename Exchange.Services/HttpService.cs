using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exchange.Services
{
    public class HttpService
    {
        private readonly IHttpClientFactory _client;

        public HttpService(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<T> Get<T>(string url) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}");

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }      
            return null;
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _client;

        public HttpHelper(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);
            return response;
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;

namespace API.Helpers
{
    public interface IHttpHelper
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}

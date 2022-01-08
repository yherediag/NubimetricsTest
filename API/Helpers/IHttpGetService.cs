using System.Net.Http;
using System.Threading.Tasks;

namespace API.Helpers
{
    public interface IHttpGetService
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}

using API.Helpers;
using System.Threading.Tasks;

namespace API.Strategies
{
    public interface IPaisesStrategy
    {
        public Task<object> TraerPais(string idPais, IHttpGetService httpHelper);
    }
}

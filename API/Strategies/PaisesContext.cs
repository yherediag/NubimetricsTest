using API.Helpers;
using System.Threading.Tasks;

namespace API.Strategies
{
    public class PaisesContext
    {
        private IPaisesStrategy _paisesStrategy;

        public IPaisesStrategy PaisesStrategy { set { _paisesStrategy = value; } }

        public PaisesContext(IPaisesStrategy strategy)
        {
            _paisesStrategy = strategy;
        }

        public async Task<object> TraerPais(string idPais, IHttpGetService httpHelper)
        {
            return await _paisesStrategy.TraerPais(idPais, httpHelper);
        }
    }
}

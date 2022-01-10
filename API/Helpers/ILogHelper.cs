using System.Collections;
using System.Threading.Tasks;

namespace API.Helpers
{
    public interface ILogHelper
    {
        public Task Guardar(string texto);

        public Task GuardarCSV(IEnumerable lista);

        public Task Error(string texto);
    }
}

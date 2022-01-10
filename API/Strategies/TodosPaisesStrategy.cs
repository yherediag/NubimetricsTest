using API.DTOs;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Strategies
{
    public class TodosPaisesStrategy : IPaisesStrategy
    {
        private const string URL_API = "https://api.mercadolibre.com/classified_locations/countries";

        public async Task<object> TraerPais(string idPais, IHttpHelper httpHelper)
        {
            var respuesta = await httpHelper.GetAsync(URL_API);
            if (!respuesta.IsSuccessStatusCode)
                throw new Exception("Error al obtener paises");

            var listadoPaisesJson = await respuesta.Content.ReadAsStringAsync();
            var paises = JsonSerializer.Deserialize<List<PaisesDto>>(listadoPaisesJson);

            var pais = paises.Where(pais => pais.id.Equals(idPais, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (pais == null)
                throw new Exception("País no encontrado") { HResult = 404 };

            return pais;
        }
    }
}

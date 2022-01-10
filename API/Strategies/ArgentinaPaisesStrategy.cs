using API.DTOs;
using API.Helpers;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Strategies
{
    public class ArgentinaPaisesStrategy : IPaisesStrategy
    {
        private const string URL_API = "https://api.mercadolibre.com/classified_locations/countries";

        public async Task<object> TraerPais(string idPais, IHttpGetService httpHelper)
        {
            var respuesta = await httpHelper.GetAsync($"{URL_API}/{idPais}");
            if (!respuesta.IsSuccessStatusCode)
                throw new Exception("Error al obtener país");

            var argentinaJson = await respuesta.Content.ReadAsStringAsync();
            var argentinaDto = JsonSerializer.Deserialize<ArgentinaDto>(argentinaJson);

            return argentinaDto;
        }
    }
}

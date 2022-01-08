using API.DTOs;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisesController : Controller
    {
        private readonly IHttpGetService _httpHelper;
        private readonly IConfiguration _config;
        private const string URL_API = "https://api.mercadolibre.com/classified_locations/countries";

        public PaisesController(IHttpGetService httpHelper, IConfiguration config)
        {
            _httpHelper = httpHelper;
            _config = config;
        }

        [HttpGet("{idPais}", Name = "ObtenerPais")]
        public async Task<ActionResult> Get(string idPais)
        {
            // Validaciones
            if (idPais.Length != 2)
                return BadRequest(new { Message = "Código de país invalido" });

            var paisesDesautorizados = _config.GetValue<string>("UnauthorizedCountries");
            if (paisesDesautorizados.IndexOf(idPais, StringComparison.InvariantCultureIgnoreCase) > -1)
                return Unauthorized(new { Message = "Código de país no autorizado" });

            // Llamado Externo
            if (idPais.Equals("AR", StringComparison.InvariantCultureIgnoreCase))
            {
                var respuesta = await _httpHelper.GetAsync($"{URL_API}/AR");
                if (!respuesta.IsSuccessStatusCode)
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Error al obtener país" });

                var argentinaJson = await respuesta.Content.ReadAsStringAsync();
                var argentina = JsonSerializer.Deserialize<ArgentinaDto>(argentinaJson);

                return Ok(argentina);
            }
            else
            {
                var respuesta = await _httpHelper.GetAsync(URL_API);
                if (!respuesta.IsSuccessStatusCode)
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Error al obtener paises" });

                var listadoPaisesJson = await respuesta.Content.ReadAsStringAsync();
                var paises = JsonSerializer.Deserialize<List<PaisesDto>>(listadoPaisesJson);

                var pais = paises.Where(pais => pais.id.Equals(idPais, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (pais == null)
                    return NotFound(new { Message = "País no encontrado" });

                return Ok(pais);
            }  
        }
    }
}

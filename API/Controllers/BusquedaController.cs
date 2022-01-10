using API.DTOs;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusquedaController : Controller
    {
        private readonly IHttpHelper _httpHelper;
        private const string URL_API = "https://api.mercadolibre.com/sites/MLA/search";

        public BusquedaController(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        [HttpGet("{termino}")]
        public async Task<ActionResult> Get(string termino)
        {
            var respuesta = await _httpHelper.GetAsync($"{URL_API}?q={termino}"); 
            if (!respuesta.IsSuccessStatusCode)
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Error al solicitar búsqueda" });

            var busquedaJson = await respuesta.Content.ReadAsStringAsync();
            var busqueda = JsonSerializer.Deserialize<BusquedaDto>(busquedaJson);

            return Ok(busqueda);
        }
    }
}

using API.Helpers;
using API.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisesController : Controller
    {
        private readonly IHttpHelper _httpHelper;
        private readonly IConfiguration _config;

        public PaisesController(IHttpHelper httpHelper, IConfiguration config)
        {
            _httpHelper = httpHelper;
            _config = config;
        }

        [HttpGet("{idPais}", Name = "ObtenerPais")]
        public async Task<ActionResult> Get(string idPais)
        {
            try
            {
                ValidacionesHelper.ValidarIdPais(idPais, _config);

                var paisesContext = idPais.Equals("AR", StringComparison.InvariantCultureIgnoreCase) ?
                    new PaisesContext(new ArgentinaPaisesStrategy()) :
                    new PaisesContext(new TodosPaisesStrategy());

                var pais = await paisesContext.TraerPais(idPais, _httpHelper);

                return Ok(pais);
            }
            catch (Exception ex)
            {
                return ex.HResult switch
                {
                    400 => BadRequest(new { ex.Message }),
                    401 => Unauthorized(new { ex.Message }),
                    404 => NotFound(new { ex.Message }),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message }),
                };
            }
        }
    }
}

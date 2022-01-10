using API.DTOs;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisasController : Controller
    {
        private readonly IHttpHelper _httpHelper;
        private readonly ILogHelper _logHelper;
        private const string URL_CURRENCIES_API = "https://api.mercadolibre.com/currencies";
        private const string URL_CURRENCYCONVERSION_API = "https://api.mercadolibre.com/currency_conversions";

        public DivisasController(IHttpHelper httpHelper, ILogHelper logHelper)
        {
            _httpHelper = httpHelper;
            _logHelper = logHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var responseDivisas = await _httpHelper.GetAsync(URL_CURRENCIES_API);
                if (!responseDivisas.IsSuccessStatusCode)
                    throw new Exception("Error al buscar divisas");

                var divisasJson = await responseDivisas.Content.ReadAsStringAsync();
                var divisas = JsonSerializer.Deserialize<List<DivisasDto>>(divisasJson);

                await _logHelper.Guardar(divisasJson);
                await _logHelper.GuardarCSV(await CalcularRatioDivisas(divisas));

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }


        private async Task<IList<double>> CalcularRatioDivisas(List<DivisasDto> divisas)
        {
            var listRatios = new List<double>();

            foreach (var divisa in divisas)
            {
                var responseConversion = await _httpHelper.GetAsync($"{URL_CURRENCYCONVERSION_API}/search?from={divisa.id}&to=USD");
                if (!responseConversion.IsSuccessStatusCode)
                    await _logHelper.Error($"El {divisa.description} generó el siguiente codigo de estado: {responseConversion.StatusCode}");

                var conversionJson = await responseConversion.Content.ReadAsStringAsync();
                var conversion = JsonSerializer.Deserialize<ConversionDto>(conversionJson);

                listRatios.Add(conversion.ratio);
            }

            return listRatios;
        }
    }
}

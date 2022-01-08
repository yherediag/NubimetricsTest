using API.Controllers;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API.Tests.UnitTests
{
    internal class BusquedaControllerTest : BasePruebas
    {
        [Test]
        public async Task ObtenerBusquedaPorTermino()
        {
            // Preparación
            var httpHelper = ConstruirHttpHelper();
            var termino = "iphone";

            // Prueba
            var controller = new BusquedaController(httpHelper);
            var respuesta = await controller.Get(termino) as OkObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 200);

            var busqueda = (BusquedaDto)respuesta.Value;
            Assert.AreEqual(termino, busqueda.query);
        }
    }
}

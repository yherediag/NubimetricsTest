using API.Controllers;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.UnitTests
{
    internal class PaisesControllerTest : BasePruebas
    {
        [Test]
        [TestCase("AR")]
        public async Task ObtenerPaisArgentina(string idPais)
        {
            // Preparación
            var httpHelper = ConstruirHttpHelper();
            var config = ConstruirConfiguration();

            // Prueba
            var controller = new PaisesController(httpHelper, config);
            var respuesta = await controller.Get(idPais) as OkObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 200);

            var pais = (ArgentinaDto)respuesta.Value;
            Assert.AreEqual(idPais, pais.id);
            Assert.IsTrue(pais.states.Any(state => state.name.Contains("Capital Federal")));
        }

        [Test]
        [TestCase("VE")]
        [TestCase("BO")]
        [TestCase("PE")]
        public async Task ObtenerPaisPorIdExistente(string idPais)
        {
            // Preparación
            var httpHelper = ConstruirHttpHelper();
            var config = ConstruirConfiguration();

            // Prueba
            var controller = new PaisesController(httpHelper, config);
            var respuesta = await controller.Get(idPais) as OkObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 200);

            var pais = (PaisesDto)respuesta.Value;
            Assert.AreEqual(idPais, pais.id);
        }

        [Test]
        [TestCase("GG")]
        [TestCase("OP")]
        public async Task ObtenerPaisPorIdInexistente(string idPais)
        {
            // Preparación
            var httpHelper = ConstruirHttpHelper();
            var config = ConstruirConfiguration();

            // Prueba
            var controller = new PaisesController(httpHelper, config);
            var respuesta = await controller.Get(idPais) as NotFoundObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 404);
            Assert.AreEqual(respuesta.Value.ToString(), new { Message = "País no encontrado" }.ToString());
        }

        [Test]
        [TestCase("BR")]
        [TestCase("CO")]
        public async Task DesautorizarIdExistente(string idPais)
        {
            // Preparación
            var httpHelper = ConstruirHttpHelper();
            var config = ConstruirConfiguration();

            // Prueba
            var controller = new PaisesController(httpHelper, config);
            var respuesta = await controller.Get(idPais) as UnauthorizedObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 401);
            Assert.AreEqual(respuesta.Value.ToString(), new { Message = "Código de país no autorizado" }.ToString());
        }

        [Test]
        [TestCase("AAA")]
        [TestCase("123")]
        [TestCase("ppppp")]
        [TestCase("")]
        [TestCase(null)]
        public async Task ObtenerPaisPorIdErroneo(string idPais)
        {
            // Preparación
            var httpHelper = ConstruirHttpHelper();
            var config = ConstruirConfiguration();

            // Prueba
            var controller = new PaisesController(httpHelper, config);
            var respuesta = await controller.Get(idPais) as BadRequestObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 400);
            Assert.AreEqual(respuesta.Value.ToString(), new { Message = "Código de país invalido" }.ToString());
        }
    }
}

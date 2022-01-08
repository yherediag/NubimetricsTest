using API.Controllers;
using API.DTOs;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.UnitTests
{
    public class UsuariosControllerTest : BasePruebas
    {
        [Test]
        public async Task ObtenerTodosLosUsuarios()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            contexto.Usuarios.Add(new Usuario() { Nombre = "Pedro", Apellido = "Perez", Email = "pedro@mailinator.com", Password = "123456", Habilitado = true, FechaAlta = DateTime.Now });
            contexto.Usuarios.Add(new Usuario() { Nombre = "Jose", Apellido = "Martinez", Email = "jose@mailinator.com", Password = "654321", Habilitado = true, FechaAlta = DateTime.Now });
            contexto.Usuarios.Add(new Usuario() { Nombre = "Juan", Apellido = "Garcia", Email = "juan@mailinator.com", Password = "246135", Habilitado = true, FechaAlta = DateTime.Now });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            // Prueba
            var controller = new UsuariosController(contexto2, mapper, new PasswordHelper());
            var respuesta = await controller.Get() as OkObjectResult;

            // Verificación
            Assert.AreEqual(respuesta.StatusCode, 200);

            var usuarios = (List<UsuarioDto>)respuesta.Value;
            Assert.AreEqual(3, usuarios.Count);
        }

        [Test]
        public async Task ObtenerUsuarioPorIdNoExistente()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var controller = new UsuariosController(contexto, mapper, new PasswordHelper());
            var respuesta = await controller.Get(1);

            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [Test]
        public async Task ObtenerUsuarioPorIdExistente()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            contexto.Usuarios.Add(new Usuario() { Nombre = "Pedro", Apellido = "Perez", Email = "pedro@mailinator.com", Password = "123456", Habilitado = true, FechaAlta = DateTime.Now });
            contexto.Usuarios.Add(new Usuario() { Nombre = "Jose", Apellido = "Martinez", Email = "jose@mailinator.com", Password = "654321", Habilitado = true, FechaAlta = DateTime.Now });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new UsuariosController(contexto2, mapper, new PasswordHelper());

            var id = 2;
            var respuesta = await controller.Get(id) as OkObjectResult;
            var resultado = (UsuarioDto)respuesta.Value;
            Assert.AreEqual(id, resultado.Id);
        }

        [Test]
        public async Task CrearUsuario()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var nuevoUsuario = new UsuarioPostDto() { Nombre = "NuevoNombre", Apellido = "NuevoApellido", Email = "nuevoemail@mailinator.com", Password = "123456" };
            var controller = new UsuariosController(contexto, mapper, new PasswordHelper());

            var respuesta = await controller.Post(nuevoUsuario);
            var resultado = respuesta as CreatedAtRouteResult;
            Assert.IsNotNull(resultado);

            var contexto2 = ConstruirContext(nombreBD);
            var cantidad = await contexto2.Usuarios.CountAsync();
            Assert.AreEqual(1, cantidad);
        }

        [Test]
        public async Task ActualizarUsuario()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();
            var passwordHelper = new PasswordHelper();

            contexto.Usuarios.Add(new Usuario() { Nombre = "ViejoNombre", Apellido = "ViejoApellido", Email = "viejoemail@mailinator.com", Password = passwordHelper.Encriptar("123456"), Habilitado = true, FechaAlta = DateTime.Now });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new UsuariosController(contexto2, mapper, passwordHelper);

            var usuarioPutDto = new UsuarioPutDto() { Nombre = "NuevoNombre", Apellido = "NuevoApellido", Email = "nuevoemail@mailinator.com", Password = "654321" };

            var id = 1;
            var respuesta = await controller.Put(id, usuarioPutDto);

            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(204, resultado.StatusCode);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.Usuarios.AnyAsync(x => x.Nombre == usuarioPutDto.Nombre
                                                                && x.Apellido == usuarioPutDto.Apellido
                                                                && x.Email == usuarioPutDto.Email
                                                                && x.Password == passwordHelper.Encriptar(usuarioPutDto.Password));
            Assert.IsTrue(existe);
        }

        [Test]
        public async Task IntentaBorrarUsuarioNoExistente()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var controller = new UsuariosController(contexto, mapper, new PasswordHelper());

            var respuesta = await controller.Delete(1);
            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [Test]
        public async Task BorrarUsuario()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            contexto.Usuarios.Add(new Usuario() { Nombre = "Pedro", Apellido = "Perez", Email = "pedro@mailinator.com", Password = "123456", Habilitado = true, FechaAlta = DateTime.Now });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new UsuariosController(contexto2, mapper, new PasswordHelper());

            var id = 1;
            var respuesta = await controller.Delete(id);
            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(204, resultado.StatusCode);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.Usuarios.AnyAsync(x => x.Id == id
                                                                && !x.Habilitado);
            Assert.IsTrue(existe);
        }

    }
}

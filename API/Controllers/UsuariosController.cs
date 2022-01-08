using API.DTOs;
using API.Helpers;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly NubimetricsExampleContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordHelper;

        public UsuariosController(NubimetricsExampleContext context,
                                  IMapper mapper,
                                  IPasswordService passwordHelper)
        {
            _context = context;
            _mapper = mapper;
            _passwordHelper = passwordHelper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var usuarios = await _context.Usuarios.Where(usuarios => usuarios.Habilitado).ToListAsync();

            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);

            return Ok(usuariosDto);
        }

        [HttpGet("{id}", Name = "ObtenerUsuario")]
        public async Task<ActionResult> Get(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => (usuario.Id == id) && usuario.Habilitado);

            if (usuario == null) return NotFound();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioPostDto usuarioPostDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioPostDto);

            usuario.Password = _passwordHelper.Encriptar(usuario.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return new CreatedAtRouteResult("ObtenerUsuario", new { id = usuario.Id }, usuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioPutDto usuarioPutDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => (usuario.Id == id) && usuario.Habilitado);

            if (usuario == null) return NotFound();

            var userPassword = _passwordHelper.Desencriptar(usuario.Password);

            usuario = _mapper.Map(usuarioPutDto, usuario);
            usuario.Password = usuarioPutDto.Password != userPassword ? _passwordHelper.Encriptar(usuario.Password) : usuario.Password;
            usuario.FechaModificado = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => (usuario.Id == id) && usuario.Habilitado);

            if (usuario == null) return NotFound();

            usuario.Habilitado = false;
            usuario.FechaModificado = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

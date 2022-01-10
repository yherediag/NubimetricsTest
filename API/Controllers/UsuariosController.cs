using API.DTOs;
using API.Helpers;
using API.Models.Data;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEFRepository<Usuario> _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHelper _passwordHelper;

        public UsuariosController(IEFRepository<Usuario> context,
                                  IMapper mapper,
                                  IPasswordHelper passwordHelper)
        {
            _repository = context;
            _mapper = mapper;
            _passwordHelper = passwordHelper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var usuarios = await _repository.Get();

            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios.Where(usuario => usuario.Habilitado.Value));

            return Ok(usuariosDto);
        }

        [HttpGet("{id}", Name = "ObtenerUsuario")]
        public async Task<ActionResult> Get(int id)
        {
            var usuario = await _repository.Get(id);

            if (usuario == null || !usuario.Habilitado.Value) return NotFound();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioPostDto usuarioPostDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioPostDto);

            usuario.Password = _passwordHelper.Encriptar(usuario.Password);

            await _repository.Post(usuario);
            await _repository.Save();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return new CreatedAtRouteResult("ObtenerUsuario", new { id = usuario.Id }, usuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioPutDto usuarioPutDto)
        {
            var usuario = await _repository.Get(id);

            if (usuario == null || !usuario.Habilitado.Value) return NotFound();

            var userPassword = _passwordHelper.Desencriptar(usuario.Password);

            usuario = _mapper.Map(usuarioPutDto, usuario);
            usuario.Password = usuarioPutDto.Password != userPassword ? _passwordHelper.Encriptar(usuario.Password) : usuario.Password;
            usuario.FechaModificado = DateTime.Now;

            await _repository.Put(usuario);
            await _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _repository.Get(id);

            if (usuario == null || !usuario.Habilitado.Value) return NotFound();

            usuario.Habilitado = false;
            usuario.FechaModificado = DateTime.Now;

            await _repository.Put(usuario);
            await _repository.Save();

            return NoContent();
        }
    }
}

using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMappingHelper : Profile
    {
        public AutoMappingHelper()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<UsuarioPostDto, Usuario>();
            CreateMap<UsuarioPutDto, Usuario>();
        }
    }
}

using API.DTOs;
using API.Models.Data;
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

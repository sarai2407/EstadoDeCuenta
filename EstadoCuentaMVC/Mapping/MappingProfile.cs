using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Data.Models;
using EstadoCuentaMVC.Models;

namespace EstadoCuentaMVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //tarjetas
            CreateMap<TarjetaDto, TarjetaViewModel>();
            CreateMap<TarjetaViewModel, TarjetaDto>();
            CreateMap<TarjetaVariablesDto, TarjetaVariablesViewModel>();
            CreateMap<TarjetaVariablesViewModel, TarjetaVariablesDto>();

            //usuarios
            CreateMap<UsuarioViewModel, UsuarioDto>();
            CreateMap<UsuarioDto, UsuarioViewModel>();

        }
    }
}

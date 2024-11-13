using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Data.Models;

namespace EstadoCuenta.Api.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Mapeo de Usuario a UsuarioDto
            CreateMap<Usuario, UsuarioDto>();
            // Mapeo de UsuarioDto a Usuario (crear un nuevo Usuario desde el DTO)
            CreateMap<UsuarioDto, Usuario>();
            //transacciones
            CreateMap<Transaccion, TransaccionDto>();
            CreateMap<TransaccionDto, Transaccion>();
            //tarjetas
            CreateMap<Tarjeta, TarjetaDto>();
            CreateMap<TarjetaDto, Tarjeta>();
            //tipotransaccion
            CreateMap<TipoTransaccion, TipoTransaccionDto>();
            CreateMap<TipoTransaccionDto, TipoTransaccion>();

        }
    }
}

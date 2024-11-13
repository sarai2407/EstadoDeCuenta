using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Data.Models;

namespace EstadoCuenta.Api.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<int> CrearUsuarioAsync(Usuario usuario);
    }
}

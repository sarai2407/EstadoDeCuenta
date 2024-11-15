using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Data.Models;
using FluentResults;

namespace EstadoCuenta.Api.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<int> CrearUsuarioAsync(Usuario usuario);
        Task<Result<Usuario>> GetUsuarioByIdAsync(int id);
    }
}

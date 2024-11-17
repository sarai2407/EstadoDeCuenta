using EstadoCuenta.Data.Models;
using FluentResults;

namespace EstadoCuenta.Api.Interfaces
{
    public interface ITransaccionRepositorio
    {
        Task<int> CrearTransaccionAsync(Transaccion transaccion);
        Task<Result<List<Transaccion>>> GetTransaccionesByNumeroAsync(string numTarjeta);
        Task<Result<List<Transaccion>>> GetTransaccionesByNumeroMesAsync(string numTarjeta);
        Task<Result<List<Transaccion>>> GetComprasByNumeroAsync(string numTarjeta);
        Task<Result<List<Transaccion>>> GetComprasMesByNumeroAsync(string numTarjeta);
        Task<Result<List<Transaccion>>> GetPagosByNumeroAsync(string numTarjeta);
    }
}

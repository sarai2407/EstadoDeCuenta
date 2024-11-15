using EstadoCuenta.Data.Models;
using FluentResults;

namespace EstadoCuenta.Api.Interfaces
{
    public interface ITarjetaRepositorio
    {
        Task<int> CrearTarjetaAsync(Tarjeta tarjeta);
        Task<Result<Tarjeta>> GetTarjetaByNumeroAsync(string numTarjeta);
        Task<bool> UpdateSaldoTarjetaAsync(string numTarjeta, decimal newSaldo);
    }
}

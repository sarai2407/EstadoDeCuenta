using EstadoCuenta.Data.Models;

namespace EstadoCuenta.Api.Interfaces
{
    public interface ITarjetaRepositorio
    {
        Task<int> CrearTarjetaAsync(Tarjeta tarjeta);
        Task<Tarjeta?> GetTarjetaByNumeroAsync(string numTarjeta);
        Task<bool> UpdateSaldoTarjetaAsync(string numTarjeta, decimal newSaldo);
    }
}

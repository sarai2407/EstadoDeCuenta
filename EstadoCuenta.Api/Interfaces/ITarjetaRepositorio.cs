using EstadoCuenta.Data.Models;

namespace EstadoCuenta.Api.Interfaces
{
    public interface ITarjetaRepositorio
    {
        Task<int> CrearTarjetaAsync(Tarjeta tarjeta);
    }
}

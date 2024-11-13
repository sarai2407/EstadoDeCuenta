using EstadoCuenta.Data.Models;

namespace EstadoCuenta.Api.Interfaces
{
    public interface ITipoTransaccionRepositorio
    {
        Task<int> CrearTipoTransaccionAsync(TipoTransaccion tipotransaccion);
    }
}

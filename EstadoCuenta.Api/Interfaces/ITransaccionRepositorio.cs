using EstadoCuenta.Data.Models;

namespace EstadoCuenta.Api.Interfaces
{
    public interface ITransaccionRepositorio
    {
        Task<int> CrearTransaccionAsync(Transaccion transaccion);
    }
}

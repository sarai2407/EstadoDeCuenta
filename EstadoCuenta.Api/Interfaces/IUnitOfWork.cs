namespace EstadoCuenta.Api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepositorio Usuarios { get; }
        ITarjetaRepositorio Tarjetas {  get; }
        ITransaccionRepositorio Transacciones { get; }
        ITipoTransaccionRepositorio TipoTransacciones { get; }
        IGenerarPdfs generarPdfs { get; }
        Task<int> SaveAsync();
    }
}

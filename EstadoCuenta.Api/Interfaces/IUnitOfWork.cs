namespace EstadoCuenta.Api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepositorio Usuarios { get; }
        Task<int> SaveAsync();
    }
}

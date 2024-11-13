using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.Repositories;
using EstadoCuenta.Data;

namespace EstadoCuenta.Api.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EstadoCuentaContext _context;
        private IUsuarioRepositorio _usuarioRepositorio;

        public UnitOfWork(EstadoCuentaContext context)
        {
            _context = context;
        }

        public IUsuarioRepositorio Usuarios => _usuarioRepositorio ??= new UsuarioRepositorio(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

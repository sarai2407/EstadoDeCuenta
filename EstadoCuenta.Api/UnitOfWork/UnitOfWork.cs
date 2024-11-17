using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.iTextSharp;
using EstadoCuenta.Api.Repositories;
using EstadoCuenta.Data;

namespace EstadoCuenta.Api.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EstadoCuentaContext _context;
        private IUsuarioRepositorio _usuarioRepositorio;
        private ITarjetaRepositorio _tarjetaRepositorio;
        private ITransaccionRepositorio _transaccionRepositorio;
        private ITipoTransaccionRepositorio _tipotransaccionRepositorio;
        private IGenerarPdfs _generarPdfs;

        public UnitOfWork(EstadoCuentaContext context)
        {
            _context = context;
        }

        public IUsuarioRepositorio Usuarios => _usuarioRepositorio ??= new UsuarioRepositorio(_context);
        public ITarjetaRepositorio Tarjetas => _tarjetaRepositorio ??= new TarjetaRepositorio(_context);
        public ITransaccionRepositorio Transacciones => _transaccionRepositorio ??= new TransaccionRepositorio(_context);
        public ITipoTransaccionRepositorio TipoTransacciones => _tipotransaccionRepositorio ??= new TipoTransaccionRepositorio(_context);
        public IGenerarPdfs generarPdfs => _generarPdfs ??= new GenerarPdfs();


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

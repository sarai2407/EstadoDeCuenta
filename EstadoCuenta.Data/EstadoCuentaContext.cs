using EstadoCuenta.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuenta.Data
{
    public class EstadoCuentaContext : DbContext
    {
        public EstadoCuentaContext(DbContextOptions<EstadoCuentaContext> options) : base(options) { }

        //Dbsets
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<TipoTransaccion> TipoTransacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Tarjeta>().ToTable("Tarjeta");
            modelBuilder.Entity<Transaccion>().ToTable("Transaccion");
            modelBuilder.Entity<TipoTransaccion>().ToTable("TipoTransaccion");
        }
    }
}

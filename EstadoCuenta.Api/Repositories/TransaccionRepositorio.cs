using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data.Models;
using EstadoCuenta.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace EstadoCuenta.Api.Repositories
{
    public class TransaccionRepositorio: ITransaccionRepositorio
    {
        private readonly EstadoCuentaContext _context;

        public TransaccionRepositorio(EstadoCuentaContext context)
        {
            _context = context;
        }

        public async Task<int> CrearTransaccionAsync(Transaccion transaccion)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Fecha", SqlDbType.DateTime2) { Value = transaccion.Fecha },
                    new SqlParameter("@Monto", SqlDbType.Decimal) { Value = transaccion.Monto },
                    new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = transaccion.Descripcion ?? (object)DBNull.Value },
                    new SqlParameter("@IdTipoTransaccion", SqlDbType.Int) { Value = transaccion.IdTipoTransaccion },
                    new SqlParameter("@NumTarjeta", SqlDbType.NVarChar) { Value = transaccion.NumTarjeta ?? (object)DBNull.Value }
                };

                // Ejecutar el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC CrearTransaccion @Fecha, @Monto, @Descripcion, @IdTipoTransaccion, @NumTarjeta", parameters);
                return result;
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepción específica para SQL
                // Puedes registrar el error aquí o devolver un valor que indique un fallo
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                return -1; // Valor indicando que hubo un error
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción
                Console.WriteLine($"Error general: {ex.Message}");
                return -1; // Valor indicando que hubo un error
            }
        }
    }
}

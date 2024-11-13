using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data.Models;
using EstadoCuenta.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace EstadoCuenta.Api.Repositories
{
    public class TipoTransaccionRepositorio : ITipoTransaccionRepositorio
    {
        private readonly EstadoCuentaContext _context;

        public TipoTransaccionRepositorio(EstadoCuentaContext context)
        {
            _context = context;
        }

        public async Task<int> CrearTipoTransaccionAsync(TipoTransaccion tipotransaccion)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@TipoDeTransaccion", SqlDbType.NVarChar) { Value = tipotransaccion.TipoDeTransaccion ?? (object)DBNull.Value },
                };

                // Ejecutar el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC CrearTipoTransaccion @TipoDeTransaccion", parameters);
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

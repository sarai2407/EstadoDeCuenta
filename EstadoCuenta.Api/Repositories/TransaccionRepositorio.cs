using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data.Models;
using EstadoCuenta.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentResults;

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
                    new SqlParameter("@NumTarjeta", SqlDbType.NVarChar) { Value = transaccion.NumTarjeta ?? (object)DBNull.Value },
                    new SqlParameter("@SaldoDisponible", SqlDbType.Decimal) { Value = transaccion.SaldoDisponible }
                };

                // Ejecutar el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC CrearTransacciones @Fecha, @Monto, @Descripcion, @IdTipoTransaccion, @NumTarjeta, @SaldoDisponible",
                    parameters);

                // Verificar si la ejecución fue exitosa
                if (result > 0)
                {
                    // Si la inserción fue exitosa, result debería ser el número de filas afectadas
                    return result;
                }
                else
                {
                    // Si no se afectaron filas, podría indicar un problema con los datos o la ejecución
                    Console.WriteLine("No se insertaron filas.");
                    return 0; // Valor indicando que no hubo filas afectadas
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepción específica para SQL
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                return -1; // Valor indicando que hubo un error SQL
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción
                Console.WriteLine($"Error general: {ex.Message}");
                return -1; // Valor indicando que hubo un error general
            }
        }

        public async Task<Result<List<Transaccion>>> GetTransaccionesByNumeroAsync(string numTarjeta)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                List<Transaccion> transacciones = await _context.Transacciones
                                                  .Where(t => t.NumTarjeta == numTarjeta)
                                                  .ToListAsync();
                if (transacciones == null || !transacciones.Any())
                {
                    return Result.Fail<List<Transaccion>>("No se encontraron transacciones para el número de tarjeta especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(transacciones);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<Transaccion>>("Ocurrió un error al obtener las transacciones.");
            }
        }

        public async Task<Result<List<Transaccion>>> GetTransaccionesByNumeroMesAsync(string numTarjeta)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                List<Transaccion> transacciones = await _context.Transacciones
                                                  .Where(t => t.NumTarjeta == numTarjeta &&
                                                t.Fecha.Month == DateTime.Now.Month &&
                                                t.Fecha.Year == DateTime.Now.Year)
                                                  .ToListAsync();
                if (transacciones == null || !transacciones.Any())
                {
                    return Result.Fail<List<Transaccion>>("No se encontraron transacciones para el número de tarjeta especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(transacciones);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<Transaccion>>("Ocurrió un error al obtener las transacciones.");
            }
        }

        public async Task<Result<List<Transaccion>>> GetComprasByNumeroAsync(string numTarjeta)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                List<Transaccion> transacciones = await _context.Transacciones
                                                  .Where(t => t.NumTarjeta == numTarjeta && t.IdTipoTransaccion == 1)
                                                  .ToListAsync();
                if (transacciones == null || !transacciones.Any())
                {
                    return Result.Fail<List<Transaccion>>("No se encontraron compras para el número de tarjeta especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(transacciones);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<Transaccion>>("Ocurrió un error al obtener las compras.");
            }
        }

        public async Task<Result<List<Transaccion>>> GetComprasMesByNumeroAsync(string numTarjeta)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                List<Transaccion> transacciones = await _context.Transacciones
                                                  .Where(t => t.NumTarjeta == numTarjeta && t.IdTipoTransaccion == 1 &&
                                                t.Fecha.Month == DateTime.Now.Month &&
                                                t.Fecha.Year == DateTime.Now.Year)
                                                  .ToListAsync();
                if (transacciones == null || !transacciones.Any())
                {
                    return Result.Fail<List<Transaccion>>("No se encontraron compras para el número de tarjeta especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(transacciones);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<Transaccion>>("Ocurrió un error al obtener las compras.");
            }
        }

        public async Task<Result<List<Transaccion>>> GetPagosByNumeroAsync(string numTarjeta)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                List<Transaccion> transacciones = await _context.Transacciones
                                                  .Where(t => t.NumTarjeta == numTarjeta && t.IdTipoTransaccion == 2)
                                                  .ToListAsync();
                if (transacciones == null || !transacciones.Any())
                {
                    return Result.Fail<List<Transaccion>>("No se encontraron pagos para el número de tarjeta especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(transacciones);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<Transaccion>>("Ocurrió un error al obtener los pagos.");
            }
        }

        public async Task<Result<(decimal totalMesActual, decimal totalMeses)>> GetSumaMontosUltimosMesesAsync(string numTarjeta)
        {
            try
            {
                // montos mes actual
                var montosMesActual = await _context.Transacciones
                    .Where(t => t.NumTarjeta == numTarjeta
                            && t.IdTipoTransaccion == 1
                            && t.Fecha.Month == DateTime.Now.Month
                            && t.Fecha.Year == DateTime.Now.Year)
                    .Select(t => t.Monto)
                    .ToListAsync();

                decimal totalMesActual = montosMesActual.Sum(); 

                // montos mes anterior
                var montosMesAnterior = await _context.Transacciones
                    .Where(t => t.NumTarjeta == numTarjeta
                            && t.IdTipoTransaccion == 1
                            && t.Fecha.Month == DateTime.Now.AddMonths(-1).Month
                            && t.Fecha.Year == DateTime.Now.AddMonths(-1).Year)
                    .Select(t => t.Monto)
                    .ToListAsync();

                decimal totalMesAnterior = montosMesAnterior.Sum();

                decimal totalMeses = totalMesActual + totalMesAnterior;

                // tuple con los totales
                return Result.Ok((totalMesActual, totalMeses));
            }
            catch (Exception ex)
            {
                return Result.Fail<(decimal, decimal)>("Ocurrió un error al obtener los montos.");
            }
        }

    }
}

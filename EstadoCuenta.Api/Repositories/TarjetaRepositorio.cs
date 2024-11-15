using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data;
using EstadoCuenta.Data.Models;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using FluentResults;

namespace EstadoCuenta.Api.Repositories
{
    public class TarjetaRepositorio: ITarjetaRepositorio
    {
        private readonly EstadoCuentaContext _context;

        public TarjetaRepositorio(EstadoCuentaContext context)
        {
            _context = context;
        }

        public async Task<int> CrearTarjetaAsync(Tarjeta tarjeta)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@NumTarjeta", SqlDbType.NVarChar) { Value = tarjeta.NumTarjeta },
                    new SqlParameter("@LimiteCredito", SqlDbType.Decimal) { Value = tarjeta.LimiteCredito },
                    new SqlParameter("@Saldo", SqlDbType.Decimal) { Value = tarjeta.Saldo },
                    new SqlParameter("@SaldoDisponible", SqlDbType.Decimal) { Value = tarjeta.SaldoDisponible },
                    new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = tarjeta.IdUsuario }
                };

                // Ejecutar el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC CrearTarjeta @NumTarjeta, @LimiteCredito, @Saldo, @SaldoDisponible, @IdUsuario", parameters);
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

        public async Task<Result<Tarjeta>> GetTarjetaByNumeroAsync(string numTarjeta)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                Tarjeta tarjeta = await _context.Tarjetas
                                 .FirstOrDefaultAsync(t => t.NumTarjeta == numTarjeta);

                if (tarjeta == null)
                {
                    return Result.Fail("No se encontraron pagos para el número de tarjeta especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return Result.Fail<Tarjeta>("Ocurrió un error al obtener la tarjeta.");
            }

        }

        public async Task<bool> UpdateSaldoTarjetaAsync(string numTarjeta, decimal newSaldo)
        {
            var tarjeta = await _context.Tarjetas
                                 .FirstOrDefaultAsync(t => t.NumTarjeta == numTarjeta);

            if (tarjeta != null)
            { 
                tarjeta.Saldo = newSaldo;
                tarjeta.SaldoDisponible = tarjeta.LimiteCredito - tarjeta.Saldo;
                await _context.SaveChangesAsync();
                return true; // Éxito en la actualización
            }
            return false; // No se encontró la tarjeta
        }
    }
}

using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data;
using EstadoCuenta.Data.Models;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EstadoCuenta.Api.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly EstadoCuentaContext _context;

        public UsuarioRepositorio(EstadoCuentaContext context)
        {
            _context = context;
        }

        public async Task<int> CrearUsuarioAsync(Usuario usuario)
        {
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = usuario.Nombre },
                new SqlParameter("@Apellidos", SqlDbType.NVarChar) { Value = usuario.Apellidos },
                new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = usuario.Telefono },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = usuario.Email },
                new SqlParameter("@Dui", SqlDbType.NVarChar) { Value = usuario.Dui },
                new SqlParameter("@FechaRegistro", SqlDbType.DateTime2) { Value = usuario.FechaRegistro }
                };

                // Ejecutar el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC CrearUsuario @Nombre, @Apellidos, @Telefono, @Email, @Dui, @FechaRegistro", parameters);
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

        public async Task<Result<Usuario>> GetUsuarioByIdAsync(int id)
        {
            try
            {
                // Filtra todas las transacciones por el número de tarjeta
                Usuario usuario = await _context.Usuarios
                                 .FirstOrDefaultAsync(t => t.IdUsuario == id);

                if (usuario == null)
                {
                    return Result.Fail("No se encontraron usuarios con el Id especificado.");
                }

                // Si no hay transacciones, retornamos una lista vacía
                return Result.Ok(usuario);
            }
            catch (Exception ex)
            {
                return Result.Fail<Usuario>("Ocurrió un error al obtener el usuario.");
            }

        }

        public async Task<Result<List<Usuario>>> GetAllUsersAsync()
        {
            try
            {
                List<Usuario> usuarios = await _context.Usuarios.ToListAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return Result.Fail<List<Usuario>>("No se encontraron usuarios.");
                }
                return Result.Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Result.Fail<List<Usuario>>("Ocurrió un error al obtener los usuarios.");
            }
        }

    }
}

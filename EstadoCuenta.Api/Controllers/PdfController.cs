using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.iTextSharp;
using EstadoCuenta.Data.Models;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PdfController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string NumTarjeta)
        {
            try
            {
                    Result<Tarjeta> tarjeta = await _unitOfWork.Tarjetas.GetTarjetaByNumeroAsync(NumTarjeta);
                if (!tarjeta.IsSuccess || tarjeta.Value == null)
                    return NotFound("No se encontró la tarjeta especificada.");

                Result<List<Transaccion>> transacciones = await _unitOfWork.Transacciones.GetTransaccionesByNumeroAsync(NumTarjeta);
                if (!transacciones.IsSuccess || transacciones.Value == null || !transacciones.Value.Any())
                    return NotFound("No se encontraron transacciones para esta tarjeta.");

                Result<Usuario> user = await _unitOfWork.Usuarios.GetUsuarioByIdAsync(tarjeta.Value.IdUsuario);
                if (!user.IsSuccess || user.Value == null)
                    return NotFound("No se encontró información del usuario.");

                var pdfTransaction = _mapper.Map<List<PdfEstadoDto>>(transacciones.Value);
                if (transacciones.Value == null || !transacciones.Value.Any())
                    return NotFound("No se encontraron transacciones para esta tarjeta.");

                datosPersona pdfestado = new datosPersona()
                {
                    NumTarjeta = NumTarjeta,
                    IdUsuario = user.Value.IdUsuario,
                    Nombre = user.Value.Nombre,
                    Apellidos = user.Value.Apellidos
                };

                var result = await _unitOfWork.generarPdfs.GenerarPdf(pdfTransaction, pdfestado, _hostEnvironment);
                if (!result.IsSuccess)
                    return StatusCode(500, "Ocurrió un error al generar el PDF");

                // Devolver el PDF como archivo descargable
                string fileName = $"{pdfestado.FechaDescarga}_EstadoCuenta.pdf";
                return File(result.Value, "application/pdf", fileName);
            }
            catch (HttpRequestException httpEx)
            {
                // Captura errores relacionados con HTTP (por ejemplo, problemas de conexión o respuesta inválida)
                return StatusCode(500, $"Error en la solicitud HTTP: {httpEx.Message}");
            }
            catch (TaskCanceledException)
            {
                // Captura errores por timeout
                return StatusCode(408, "La solicitud ha excedido el tiempo de espera.");
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }


    }
}

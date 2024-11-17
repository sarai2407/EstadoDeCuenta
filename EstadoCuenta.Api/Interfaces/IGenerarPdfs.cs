using EstadoCuenta.Api.DTOs;
using FluentResults;

namespace EstadoCuenta.Api.Interfaces
{
    public interface IGenerarPdfs
    {
        Task<Result<byte[]>> GenerarPdf(List<PdfEstadoDto> data, datosPersona pdfEstado, IWebHostEnvironment _hostEnvironment);
    }
}

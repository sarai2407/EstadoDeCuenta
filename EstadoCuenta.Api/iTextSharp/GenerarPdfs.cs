using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using FluentResults;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Utilities;

namespace EstadoCuenta.Api.iTextSharp
{
    public class GenerarPdfs : IGenerarPdfs
    {
        public async Task<Result<byte[]>> GenerarPdf(List<PdfEstadoDto> data, datosPersona pdfEstado, IWebHostEnvironment _hostEnvironment)
        {
            string fileNameExisting = @"EstadoCuenta.pdf";
            string fileNameNew = @"" + pdfEstado.FechaDescarga.Month + "EstadoCuenta" + ".pdf";
            //string sPath = _hostEnvironment.WebRootPath + "\\";
            //string fullExistinPath = sPath + fileNameExisting;
            string sPath = Path.Combine(_hostEnvironment.WebRootPath, "templates");
            string fullExistinPath = Path.Combine(sPath, fileNameExisting);
            string folderRoute = "D:\\GuardarArchivos\\";

            if (!Directory.Exists(folderRoute))
            {
                Directory.CreateDirectory(folderRoute);
            }
            string fullNewPath = Path.Combine(folderRoute, fileNameNew);

            if (File.Exists(fullNewPath))
                File.Delete(fullNewPath);

            using (var existingFileStram = new FileStream(fullExistinPath, FileMode.Open))
            using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
            {
                var pdfReader = new PdfReader(existingFileStram);

                var stamper = new PdfStamper(pdfReader, newFileStream);

                estadoCuentaPdf.StamperPdfEstado(stamper, data, pdfEstado);

                stamper.FormFlattening = true;
                stamper.Close();
                pdfReader.Close();
            }
            byte[] bytes = await File.ReadAllBytesAsync(fullNewPath);

            return Result.Ok(bytes);
        }
    }
}

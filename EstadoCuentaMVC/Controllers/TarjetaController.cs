using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuentaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EstadoCuentaMVC.Controllers
{
    [Route("Tarjeta")]
    public class TarjetaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public TarjetaController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpGet("DetalleTarjeta")]
        // la direccion url para acceder aca es https://localhost:7032/Tarjeta/DetalleTarjeta?numTarjeta=2356897545851245
        public async Task<IActionResult> DetalleTarjeta(string numTarjeta)
        {
            if (string.IsNullOrEmpty(numTarjeta))
            {
                // Si no se pasa el número de tarjeta, devolvemos una vista con el mensaje de error
                ViewBag.ErrorMessage = "El número de tarjeta es requerido.";
                return View("DetalleTarjeta");  // Devolver la vista sin datos de la tarjeta
            }
            if (numTarjeta == "null1")
            {
                // Si no se pasa el número de tarjeta, devolvemos una vista con el mensaje de error
                ViewBag.ErrorMessage = "El Id del usuario no esta asociado a ninguna tarjeta.";
                return View("DetalleTarjeta");  // Devolver la vista sin datos de la tarjeta
            }

            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"/api/Tarjeta/GetTarjetaVariables?NumTarjeta={numTarjeta}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tarjetaVariablesDto = JsonConvert.DeserializeObject<TarjetaVariablesDto>(content);
                var tarjetaVariablesViewModel = _mapper.Map<TarjetaVariablesViewModel>(tarjetaVariablesDto);

                // Obtener los detalles del usuario asociado a la tarjeta
                var userResponse = await client.GetAsync($"/api/Usuario/GetUser?id={tarjetaVariablesViewModel.IdUsuario}");
                if (userResponse.IsSuccessStatusCode)
                {
                    var userContent = await userResponse.Content.ReadAsStringAsync();
                    var usuarioDto = JsonConvert.DeserializeObject<UsuarioDto>(userContent);
                    var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioDto);

                    // Obtener compras del mes asociadas a la tarjeta
                    var comprasResponse = await client.GetAsync($"/api/Transaccion/comprasMes?numTarjeta={numTarjeta}");
                    List<TransaccionViewModel> comprasViewModel = new();
                    if (comprasResponse.IsSuccessStatusCode)
                    {
                        var comprasContent = await comprasResponse.Content.ReadAsStringAsync();
                        var comprasDto = JsonConvert.DeserializeObject<List<TransaccionDto>>(comprasContent);
                        comprasViewModel = _mapper.Map<List<TransaccionViewModel>>(comprasDto);
                    }
                    else
                    {
                        ViewBag.WarningMessage = "No se encontraron compras realizadas este mes.";
                    }

                    informacionViewModel infor = new informacionViewModel()
                    {
                        tarjetaVariables = tarjetaVariablesViewModel,
                        usuarioinf = usuarioViewModel,
                        transacciones = comprasViewModel
                    };

                    //suma de montos
                    var MontosResponse = await client.GetAsync($"/api/Transaccion/GetSumaMontos?numTarjeta={numTarjeta}");
                    var MontosContent = await MontosResponse.Content.ReadAsStringAsync();
                    var MontosResult = JsonConvert.DeserializeObject<dynamic>(MontosContent);

                    var totalMesActual = MontosResult.totalMesActual;
                    var totalMeses = MontosResult.totalMeses;

                    ViewData["totalMesActual"] = totalMesActual;
                    ViewData["totalMeses"] = totalMeses;

                    return View("DetalleTarjeta", infor);
                }
                else
                {
                    ViewBag.ErrorMessage = "No se encontró el usuario asociado a la tarjeta.";
                    return View("DetalleTarjeta");
                }
            }

            return NotFound("No se encontró la tarjeta especificada.");
        }

        [HttpGet("DescargarPdf")]
        public async Task<IActionResult> DescargarPdf(string numTarjeta)
        {
            if (string.IsNullOrEmpty(numTarjeta))
            {
                // Si no se pasa el número de tarjeta, devolvemos un error
                return BadRequest("El número de tarjeta es requerido.");
            }

            // Aquí deberías hacer la solicitud al servicio de la API para obtener el archivo PDF.
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"/api/Pdf/DownloadFile?NumTarjeta={numTarjeta}");

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                string fileName = $"{numTarjeta}_EstadoCuenta.pdf";
                return File(fileBytes, "application/pdf", fileName);
            }

            return NotFound("No se pudo generar el PDF.");
        }
    }
}

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

                // Mapear el DTO a la ViewModel usando AutoMapper
                var tarjetaVariablesViewModel = _mapper.Map<TarjetaVariablesViewModel>(tarjetaVariablesDto);

                return View("DetalleTarjeta", tarjetaVariablesViewModel);
            }

            return NotFound("No se encontró la tarjeta especificada.");
        }
    }
}

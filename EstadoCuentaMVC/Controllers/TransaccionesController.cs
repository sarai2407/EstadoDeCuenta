using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Data.Models;
using EstadoCuentaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using AutoMapper;

namespace EstadoCuentaMVC.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public TransaccionesController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;

        }

        // GET: Vista para registrar una compra
        [HttpGet]
        public IActionResult CrearCompra(string numTarjeta)
        {
            var model = new TransaccionViewModel
            {
                NumTarjeta = numTarjeta,
                IdTipoTransaccion = 1 // Compra
            };
            return View(model); // Renderiza la vista CrearCompra
        }

        // GET: Vista para registrar un pago
        [HttpGet]
        public IActionResult CrearPago(string numTarjeta)
        {
            var model = new TransaccionViewModel
            {
                NumTarjeta = numTarjeta,
                IdTipoTransaccion = 2, // Pago
                Descripcion = "Pago de tarjeta" // Descripción predefinida
            };
            return View(model); // Renderiza la vista CrearPago
        }

        // POST: Procesa tanto compras como pagos
        [HttpPost]
        public async Task<IActionResult> Crear(TransaccionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Devuelve a la vista correspondiente si el modelo no es válido
                return View(model.IdTipoTransaccion == 1 ? "CrearCompra" : "CrearPago", model);
            }

            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.PostAsJsonAsync("/api/Transaccion/CrearTransaccion", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = model.IdTipoTransaccion == 1
                    ? "Compra registrada exitosamente."
                    : "Pago registrado exitosamente.";

                // Envía el número de tarjeta mediante TempData
                TempData["NumTarjeta"] = model.NumTarjeta;
                return RedirectToAction("Confirmacion"); // Redirige a una vista de confirmación
            }

            TempData["ErrorMessage"] = "Ocurrió un error al registrar la transacción.";
            return View(model.IdTipoTransaccion == 1 ? "CrearCompra" : "CrearPago", model);
        }

        // Vista de confirmación
        [HttpGet]
        public IActionResult Confirmacion()
        {
            // Recupera el número de tarjeta desde TempData
            ViewData["NumTarjeta"] = TempData["NumTarjeta"];

            return View(); // Renderiza una vista simple de confirmación
        }


        // Obtener las transacciones del mes por número de tarjeta
        [HttpGet("TransaccionesMes")]
        public async Task<IActionResult> TransaccionesMes(string numTarjeta)
        {
            // Hacer una solicitud GET al controlador de la API
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"/api/Transaccion/transaccionesmes?numTarjeta={numTarjeta}");

            if (response.IsSuccessStatusCode)
            {
                // Obtener la respuesta y mapearla a un modelo de la vista usando AutoMapper
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var transaccionesDto = JsonConvert.DeserializeObject<List<TransaccionDto>>(jsonResponse);

                // Usamos AutoMapper para mapear los objetos de TransaccionViewModel a TransaccionDto
                var transaccionesViewModel = _mapper.Map<List<TransaccionViewModel>>(transaccionesDto);

                TempData["NumTarjeta"] = numTarjeta;
                ViewData["NumTarjeta"] = TempData["NumTarjeta"];

                // Pasar la lista de transacciones a la vista
                return View(transaccionesViewModel);
            }

            // Si no se encuentra, mostrar un error
            ViewBag.ErrorMessage = "No se pudieron obtener las transacciones.";
            return View("Error");
        }

        [HttpGet("TransaccionesAll")]
        public async Task<IActionResult> TransaccionesAll(string numTarjeta)
        {
            // Hacer una solicitud GET al controlador de la API
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"/api/Transaccion/transaccionesAll?numTarjeta={numTarjeta}");

            if (response.IsSuccessStatusCode)
            {
                // Obtener la respuesta y mapearla a un modelo de la vista usando AutoMapper
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var transaccionesDto = JsonConvert.DeserializeObject<List<TransaccionDto>>(jsonResponse);

                // Usamos AutoMapper para mapear los objetos de TransaccionViewModel a TransaccionDto
                var transaccionesViewModel = _mapper.Map<List<TransaccionViewModel>>(transaccionesDto);

                TempData["NumTarjeta"] = numTarjeta;
                ViewData["NumTarjeta"] = TempData["NumTarjeta"];

                // Pasar la lista de transacciones a la vista
                return View(transaccionesViewModel);
            }

            // Si no se encuentra, mostrar un error
            ViewBag.ErrorMessage = "No se pudieron obtener las transacciones.";
            return View("Error");
        }
    }
}

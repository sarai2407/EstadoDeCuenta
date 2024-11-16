using EstadoCuentaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuentaMVC.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransaccionesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
    }
}

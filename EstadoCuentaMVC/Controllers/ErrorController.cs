using Microsoft.AspNetCore.Mvc;

namespace EstadoCuentaMVC.Controllers
{
    public class ErrorController : Controller
    {
        //manejar errores generales (500)
        [Route("Error")]
        public IActionResult GeneralError()
        {
            // Puedes pasar un mensaje de error a la vista si lo deseas
            ViewBag.ErrorMessage = "Ocurrió un error inesperado. Intente nuevamente más tarde.";
            return View("GeneralError");
        }

        //manejar errores 404
        [Route("Error/404")]
        public IActionResult NotFoundError()
        {
            ViewBag.ErrorMessage = "Lo sentimos, la página que buscas no existe.";
            return View("NotFound");
        }
    }
}

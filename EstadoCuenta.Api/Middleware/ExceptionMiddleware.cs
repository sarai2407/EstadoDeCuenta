using System.Net;
using System.Text.Json;

namespace EstadoCuenta.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Intentamos procesar la siguiente parte de la solicitud
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, la manejamos aquí
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log de la excepción
            _logger.LogError(exception, "Ocurrió un error no controlado.");

            // Configuramos la respuesta HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Se produjo un error en el servidor. Intente de nuevo más tarde.",
                Detailed = exception.Message // Quitar en producción para no revelar detalles
            };

            // Serializamos la respuesta en JSON
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

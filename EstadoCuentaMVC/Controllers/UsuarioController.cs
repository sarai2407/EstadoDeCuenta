using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuentaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EstadoCuentaMVC.Controllers
{
    [Route("Usuario")]
    public class UsuarioController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public UsuarioController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        // Acción para obtener la lista de usuarios
        [HttpGet("Usuarios")]
        public async Task<IActionResult> Usuarios()
        {
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync("/api/Usuario/GetUsers");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuariosDto = JsonConvert.DeserializeObject<List<UsuarioDto>>(content);

                // Mapear el DTO a ViewModel
                var usuariosViewModel = _mapper.Map<List<UsuarioViewModel>>(usuariosDto);

                return View(usuariosViewModel);
            }

            return NotFound("No se encontraron usuarios.");
        }

        // Acción para obtener la tarjeta de un usuario específico
        [HttpGet("DetalleidTarjeta")]
        public async Task<IActionResult> DetalleidTarjeta(int idUsuario)
        {
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"/api/Tarjeta/GetTarjetaIdUser?idUser={idUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tarjetaDto = JsonConvert.DeserializeObject<TarjetaDto>(content);

                // Mapear el DTO de tarjeta a ViewModel
                var tarjetaViewModel = _mapper.Map<TarjetaViewModel>(tarjetaDto);

                return RedirectToAction("DetalleTarjeta", "Tarjeta", new { numTarjeta = tarjetaViewModel.NumTarjeta });
            }

            return RedirectToAction("DetalleTarjeta", "Tarjeta", new { numTarjeta = "null1" });
        }
    }
}


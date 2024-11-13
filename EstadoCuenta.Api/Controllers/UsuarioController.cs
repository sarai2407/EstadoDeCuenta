using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.UnitOfWork;
using EstadoCuenta.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioDto usuarioDto)
        {
            //mappeo
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            //crear usuario
            var result = await _unitOfWork.Usuarios.CrearUsuarioAsync(usuario);

            if (result > 0)
            {
                return Ok(new { message = "Usuario creado exitosamente" });
            }

            return BadRequest(new { message = "Error al crear el usuario" });
        }
    }
}

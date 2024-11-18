using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.UnitOfWork;
using EstadoCuenta.Data.Models;
using FluentResults;
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
                return Ok(new { message = "Usuario creado exitosamente", idUser = result });
            }

            return BadRequest(new { message = "Error al crear el usuario" });
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            Result<Usuario> user = await _unitOfWork.Usuarios.GetUsuarioByIdAsync(id);

            if (user.IsSuccess)
            {
                var result = _mapper.Map<UsuarioDto>(user.Value);
                return Ok(result);
            }

            return NotFound(user.Errors.FirstOrDefault()?.Message);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            Result<List<Usuario>> usuarios = await _unitOfWork.Usuarios.GetAllUsersAsync();
            
            if (usuarios.IsSuccess)
            {
                var result = _mapper.Map<List<UsuarioDto>>(usuarios.Value);
                return Ok(result);
            }

            return NotFound(usuarios.Errors.FirstOrDefault()?.Message);
        }
    }
}

using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data.Models;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TarjetaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("CrearTarjeta")]
        public async Task<IActionResult> CrearTarjeta([FromBody]TarjetaDto tarjetaDto)
        {
            var tarjeta = _mapper.Map<Tarjeta>(tarjetaDto);

            var result = await _unitOfWork.Tarjetas.CrearTarjetaAsync(tarjeta);

            if (result > 0)
            {
                return Ok(new { message = "Tarjeta creada exitosamente" });
            }

            return BadRequest(new { message = "Error al crear la tarjeta" });
        }

        [HttpGet("GetTarjetaNum")]
        public async Task<IActionResult> GetTarjetaNum(string NumTarjeta)
        {
            Result<Tarjeta> tarjeta =  await _unitOfWork.Tarjetas.GetTarjetaByNumeroAsync(NumTarjeta);

            if (tarjeta.IsSuccess)
            {
                var result = _mapper.Map<TarjetaDto>(tarjeta.Value);
                return Ok(result);
            }

            return NotFound(tarjeta.Errors.FirstOrDefault().Message);
        }

        [HttpGet("GetTarjetaVariables")]
        public async Task<IActionResult> GetTarjetaVariables(string NumTarjeta)
        {
            Result<Tarjeta> tarjeta = await _unitOfWork.Tarjetas.GetTarjetaByNumeroAsync(NumTarjeta);

            if (tarjeta.IsSuccess)
            {
                var result = _mapper.Map<TarjetaVariablesDto>(tarjeta.Value);
                return Ok(result);
            }

            return NotFound(tarjeta.Errors.FirstOrDefault().Message);

        }
    }
}

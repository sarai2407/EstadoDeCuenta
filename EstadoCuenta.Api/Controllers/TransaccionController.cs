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
    public class TransaccionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransaccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("CrearTransaccion")]
        public async Task<IActionResult> CrearTransaccion([FromBody]TransaccionDto transaccionDto)
        {
            var transaccion = _mapper.Map<Transaccion>(transaccionDto);

            decimal NuevoSaldo = 0;
            var tarjeta = await _unitOfWork.Tarjetas.GetTarjetaByNumeroAsync(transaccionDto.NumTarjeta);
            decimal saldoTarjeta = tarjeta.Value.Saldo;

            switch (transaccionDto.IdTipoTransaccion)
            {
                case 1:
                    NuevoSaldo = saldoTarjeta + transaccionDto.Monto;
                    break;

                case 2:
                    NuevoSaldo = saldoTarjeta - transaccionDto.Monto;
                    break;

                default:
                    throw new ArgumentException("Tipo de transacción desconocido.");
            }
            
            transaccion.SaldoDisponible = tarjeta.Value.LimiteCredito - NuevoSaldo;

            var result = await _unitOfWork.Transacciones.CrearTransaccionAsync(transaccion);

            if (result > 0)
            {
                var updateSaldoExitoso = await _unitOfWork.Tarjetas.UpdateSaldoTarjetaAsync(transaccionDto.NumTarjeta, NuevoSaldo);

                if (updateSaldoExitoso)
                {
                    return Ok(new { message = "Transacción creada y saldo actualizado exitosamente" });
                }

                return Ok(new { message = "Transacción creada pero hubo error al actualizar saldo" });
            }

            return BadRequest(new { message = "Error al crear la transacción" });
        }

        [HttpGet("transaccionesAll")]
        public async Task<IActionResult> GetTransaccionesByNumeroAsync(string numTarjeta)
        {
            Result<List<Transaccion>> result = await _unitOfWork.Transacciones.GetTransaccionesByNumeroAsync(numTarjeta);

            if (result.IsSuccess)
            {
                var transaccion = _mapper.Map<List<TransaccionDto>>(result.Value);
                return Ok(transaccion);
            }

            return NotFound(result.Errors.FirstOrDefault().Message);

        }

        [HttpGet("transaccionesmes")]
        public async Task<IActionResult> GetTransaccioneMesByNumeroAsync(string numTarjeta)
        {
            Result<List<Transaccion>> result = await _unitOfWork.Transacciones.GetTransaccionesByNumeroMesAsync(numTarjeta);

            if (result.IsSuccess)
            {
                var transaccion = _mapper.Map<List<TransaccionDto>>(result.Value);
                return Ok(transaccion);
            }

            return NotFound(result.Errors.FirstOrDefault().Message);

        }

        [HttpGet("pagos/{numTarjeta}")]
        public async Task<IActionResult> GetPagosByNumeroAsync(string numTarjeta)
        {
            Result<List<Transaccion>> result = await _unitOfWork.Transacciones.GetPagosByNumeroAsync(numTarjeta);

            if (result.IsSuccess)
            {
                var transaccion = _mapper.Map<List<TransaccionDto>>(result.Value);
                return Ok(transaccion);
            }

            return NotFound(result.Errors.FirstOrDefault().Message);

        }

        [HttpGet("compras")]
        public async Task<IActionResult> GetComprasByNumeroAsync(string numTarjeta)
        {
            Result<List<Transaccion>> result = await _unitOfWork.Transacciones.GetComprasByNumeroAsync(numTarjeta);

            if (result.IsSuccess)
            {
                var transaccion = _mapper.Map<List<TransaccionDto>>(result.Value);
                return Ok(transaccion);
            }

            return NotFound(result.Errors.FirstOrDefault().Message);

        }

        [HttpGet("comprasMes")]
        public async Task<IActionResult> GetComprasMesByNumeroAsync(string numTarjeta)
        {
            Result<List<Transaccion>> result = await _unitOfWork.Transacciones.GetComprasMesByNumeroAsync(numTarjeta);

            if (result.IsSuccess)
            {
                var transaccion = _mapper.Map<List<TransaccionDto>>(result.Value);
                return Ok(transaccion);
            }

            return NotFound(result.Errors.FirstOrDefault().Message);

        }
    }
}

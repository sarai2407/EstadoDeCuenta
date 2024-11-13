using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Data.Models;
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

        [HttpPost]
        public async Task<IActionResult> CrearTransaccion([FromBody]TransaccionDto transaccionDto)
        {
            var transaccion = _mapper.Map<Transaccion>(transaccionDto);

            var result = await _unitOfWork.Transacciones.CrearTransaccionAsync(transaccion);

            if (result > 0)
            {
                return Ok(new { message = "Transacción creada exitosamente" });
            }

            return BadRequest(new { message = "Error al crear la transacción" });
        }
    }
}

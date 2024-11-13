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
    public class TipoTransaccionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoTransaccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoTransaccion([FromBody]TipoTransaccionDto tipoTransaccionDto)
        {
            var tipotransaccion = _mapper.Map<TipoTransaccion>(tipoTransaccionDto);

            var result = await _unitOfWork.TipoTransacciones.CrearTipoTransaccionAsync(tipotransaccion);

            if (result > 0)
            {
                return Ok(new { message = "Tipo de transacción creada exitosamente" });
            }

            return BadRequest(new { message = "Error al crear el tipo de transacción" });
        }
    }
}

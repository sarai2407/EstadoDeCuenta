using AutoMapper;
using EstadoCuenta.Api.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PdfController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string numTarjeta)
        {
            return Ok();
        }


    }
}

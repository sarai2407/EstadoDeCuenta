using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstadoCuenta.Api.DTOs
{
    public class TipoTransaccionDto
    {
        public int IdTipoTransaccion { get; set; }
        public string TipoDeTransaccion { get; set; }
    }
}

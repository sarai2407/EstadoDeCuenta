using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstadoCuenta.Api.DTOs
{
    public class TransaccionDto
    {
        public int IdTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string NumTarjeta { get; set; }
    }
}

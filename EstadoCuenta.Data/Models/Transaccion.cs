using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuenta.Data.Models
{
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }

        // Claves foráneas
        public int IdTipoTransaccion { get; set; }
        [ForeignKey("IdTipoTransaccion")]
        public virtual TipoTransaccion TipoTransacciones { get; set; }

        public string NumTarjeta { get; set; }
        [ForeignKey("NumTarjeta")]
        public virtual Tarjeta Tarjetas { get; set; }
    }
}

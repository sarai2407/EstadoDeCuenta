using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuenta.Data.Models
{
    public class TipoTransaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoTransaccion { get; set; }
        public string TipoDeTransaccion { get; set; }

        // Relación uno a muchos con Transaccion
        public virtual ICollection<Transaccion> Transacciones { get; set; }
    }
}

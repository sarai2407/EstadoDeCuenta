using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuenta.Data.Models
{
    public class Tarjeta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarjeta { get; set; }

        [Key]
        public string NumTarjeta { get; set; }

        public decimal LimiteCredito { get; set; }
        public decimal Saldo { get; set; }

        //SaldoDisponible => LimiteCredito - Saldo;
        public decimal SaldoDisponible { get; set; }
        

        // Clave foránea a Usuario
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        // Relación uno a muchos con Transaccion
        public virtual ICollection<Transaccion> Transacciones { get; set; }
    }
}

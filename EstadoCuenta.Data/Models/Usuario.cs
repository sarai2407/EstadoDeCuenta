using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuenta.Data.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Dui { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación uno a muchos con Tarjeta
        public virtual ICollection<Tarjeta> Tarjetas { get; set; }
    }
}

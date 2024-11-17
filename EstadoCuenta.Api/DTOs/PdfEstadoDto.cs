namespace EstadoCuenta.Api.DTOs
{
    public class PdfEstadoDto
    {      
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoTransaccion { get; set; }
        public decimal SaldoDisponible { get; set; }
    }

    public  class datosPersona
    {
        public string NumTarjeta { get; set; }

        public DateTime FechaDescarga = DateTime.Now;
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

    }
}

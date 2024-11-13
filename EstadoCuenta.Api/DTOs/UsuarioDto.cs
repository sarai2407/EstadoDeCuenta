namespace EstadoCuenta.Api.DTOs
{
    public class UsuarioDto
    {
        //public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Dui { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

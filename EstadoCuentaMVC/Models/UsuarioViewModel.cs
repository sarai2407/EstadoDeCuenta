﻿namespace EstadoCuentaMVC.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Dui { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

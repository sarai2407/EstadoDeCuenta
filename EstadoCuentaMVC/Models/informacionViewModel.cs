namespace EstadoCuentaMVC.Models
{
    public class informacionViewModel
    {
        public TarjetaVariablesViewModel tarjetaVariables { get; set; }
        public UsuarioViewModel usuarioinf { get; set; }
        public List<TransaccionViewModel> transacciones { get; set; }
    }
}

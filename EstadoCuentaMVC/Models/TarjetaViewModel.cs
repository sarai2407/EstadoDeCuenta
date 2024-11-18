namespace EstadoCuentaMVC.Models
{
    public class TarjetaViewModel
    {
        public int IdTarjeta { get; set; }
        public string NumTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoDisponible { get; set; }
        public int IdUsuario { get; set; }
    }
}

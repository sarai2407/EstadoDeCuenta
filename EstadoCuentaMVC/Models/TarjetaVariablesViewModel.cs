namespace EstadoCuentaMVC.Models
{
    public class TarjetaVariablesViewModel
    {
        public int IdTarjeta { get; set; }
        public string NumTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoDisponible { get; set; }
        public int IdUsuario { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal MontoTotalaPagar { get; set; }
        public decimal MontoTotalConIntereses { get; set; }
        public decimal CuotaMinimaaPagar { get; set; }
    }
}

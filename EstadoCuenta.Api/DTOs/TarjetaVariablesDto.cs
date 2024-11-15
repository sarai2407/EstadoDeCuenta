namespace EstadoCuenta.Api.DTOs
{
    public class TarjetaVariablesDto
    {
        static decimal PorcentajeInteresConfigurable = 0.25m;
        static decimal PorcentajeConfigurableSaldoMinimo = 0.05m;

        public int IdTarjeta { get; set; }
        public string NumTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoDisponible { get; set; }
        public int IdUsuario { get; set; }
        public decimal InteresBonificable => Saldo * PorcentajeInteresConfigurable;
        public decimal MontoTotalaPagar => Saldo;
        public decimal MontoTotalConIntereses => Saldo+ InteresBonificable;
        public decimal CuotaMinimaaPagar  => Saldo * PorcentajeConfigurableSaldoMinimo;


    }
}

namespace EstadoCuenta.Api.DTOs
{
    public class TarjetaDto
    {
        public int IdTarjeta { get; set; }
        public string NumTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Saldo { get; set; }

        //SaldoDisponible => LimiteCredito - Saldo;
        public decimal SaldoDisponible { get; set; }
        public int IdUsuario { get; set; }
    }
}

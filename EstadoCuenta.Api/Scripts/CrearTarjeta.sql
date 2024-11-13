CREATE PROCEDURE CrearTarjeta
    @NumTarjeta NVARCHAR(450),
    @LimiteCredito DECIMAL(18, 2),
    @Saldo DECIMAL(18, 2),
    @SaldoDisponible DECIMAL(18, 2),
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tarjeta (NumTarjeta, LimiteCredito, Saldo, SaldoDisponible, IdUsuario)
    VALUES (@NumTarjeta, @LimiteCredito, @Saldo, @SaldoDisponible, @IdUsuario);
END;
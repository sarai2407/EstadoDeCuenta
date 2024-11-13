CREATE PROCEDURE CrearTransaccion
    @Fecha DATETIME2(7),
    @Monto DECIMAL(18, 2),
    @Descripcion NVARCHAR(MAX),
    @IdTipoTransaccion INT,
    @NumTarjeta NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Transaccion (Fecha, Monto, Descripcion, IdTipoTransaccion, NumTarjeta)
    VALUES (@Fecha, @Monto, @Descripcion, @IdTipoTransaccion, @NumTarjeta);
END;
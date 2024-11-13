CREATE PROCEDURE CrearTipoTransaccion
    @TipoDeTransaccion NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO TipoTransaccion (TipoDeTransaccion)
    VALUES (@TipoDeTransaccion);
END;
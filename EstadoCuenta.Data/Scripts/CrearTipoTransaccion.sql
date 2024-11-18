CREATE PROCEDURE CrearTipoTransaccion
    @TipoDeTransaccion NVARCHAR(MAX)
AS
BEGIN

    INSERT INTO TipoTransaccion (TipoDeTransaccion)
    VALUES (@TipoDeTransaccion);
END;
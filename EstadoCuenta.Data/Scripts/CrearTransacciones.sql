USE EstadoCuentaDB
GO
/****** Object:  StoredProcedure [dbo].[CrearTransaccion]    Script Date: 16/11/2024 17:52:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[CrearTransacciones]
    @Fecha DATETIME2(7),
    @Monto DECIMAL(18, 2),
    @Descripcion NVARCHAR(MAX),
    @IdTipoTransaccion INT,
    @NumTarjeta NVARCHAR(450),
    @SaldoDisponible DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Transaccion (Fecha, Monto, Descripcion, IdTipoTransaccion, NumTarjeta, SaldoDisponible)
    VALUES (@Fecha, @Monto, @Descripcion, @IdTipoTransaccion, @NumTarjeta, @SaldoDisponible);
END;

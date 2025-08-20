CREATE PROCEDURE [dbo].[sp_Movimiento_Editar]
    @idMovimiento UNIQUEIDENTIFIER,
    @idCuenta UNIQUEIDENTIFIER,
    @idCategoria UNIQUEIDENTIFIER,
    @idTipoMovimiento UNIQUEIDENTIFIER,
    @descripcion VARCHAR(255),
    @monto DECIMAL(18,2),
    @fecha DATETIME
AS
BEGIN
    UPDATE Movimiento
    SET
        idCuenta = @idCuenta,
        idCategoria = @idCategoria,
        idTipoMovimiento = @idTipoMovimiento,
        descripcion = @descripcion,
        monto = @monto,
        fecha = @fecha
    WHERE idMovimiento = @idMovimiento;
	IF NOT EXISTS (
        SELECT 1
        FROM CuentaCategoria
        WHERE idCuenta = @idCuenta AND idCategoria = @idCategoria
    )
    BEGIN
        INSERT INTO CuentaCategoria (idCuenta, idCategoria)
        VALUES (@idCuenta, @idCategoria);
    END
END;
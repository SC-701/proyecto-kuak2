CREATE PROCEDURE sp_Movimiento_Editar
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
END;
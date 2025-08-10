CREATE PROCEDURE sp_Movimiento_Crear
    @idMovimiento UNIQUEIDENTIFIER,
    @idCuenta UNIQUEIDENTIFIER,
    @idCategoria UNIQUEIDENTIFIER,
    @idTipoMovimiento UNIQUEIDENTIFIER,
    @descripcion VARCHAR(255),
    @monto DECIMAL(18,2),
    @fecha DATETIME
AS
BEGIN
    INSERT INTO Movimiento 
        (idMovimiento, idCuenta, idCategoria, idTipoMovimiento, descripcion, monto, fecha)
    VALUES
        (@idMovimiento, @idCuenta, @idCategoria, @idTipoMovimiento, @descripcion, @monto, @fecha);

    SELECT @idMovimiento;
END;
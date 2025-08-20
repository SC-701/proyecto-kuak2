CREATE PROCEDURE [dbo].[sp_Movimiento_Crear]
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
IF NOT EXISTS (
        SELECT 1
        FROM CuentaCategoria
        WHERE IdCuenta = @idCuenta
          AND IdCategoria = @idCategoria
    )
    BEGIN
        INSERT INTO CuentaCategoria (IdCuenta, IdCategoria)
        VALUES (@idCuenta, @idCategoria);
    END

    -- Return the movimiento ID
    SELECT @idMovimiento;
END;
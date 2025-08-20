CREATE PROCEDURE [dbo].[sp_Movimiento_Eliminar]
    @idMovimiento UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @idCuenta UNIQUEIDENTIFIER;
    DECLARE @idCategoria UNIQUEIDENTIFIER;

    SELECT @idCuenta = idCuenta, 
           @idCategoria = idCategoria
    FROM Movimiento
    WHERE idMovimiento = @idMovimiento;

    DELETE FROM Movimiento
    WHERE idMovimiento = @idMovimiento;

    IF NOT EXISTS (
        SELECT 1
        FROM Movimiento
        WHERE idCuenta = @idCuenta
          AND idCategoria = @idCategoria
    )
    BEGIN
        DELETE FROM CuentaCategoria
        WHERE idCuenta = @idCuenta
          AND idCategoria = @idCategoria;
    END
END;
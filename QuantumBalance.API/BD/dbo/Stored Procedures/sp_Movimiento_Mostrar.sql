CREATE PROCEDURE sp_Movimiento_Mostrar
AS
BEGIN
    SELECT 
        idMovimiento AS IdMovimiento,
        idCuenta AS IdCuenta,
        idCategoria AS IdCategoria,
        idTipoMovimiento AS IdTipoMovimiento,
        descripcion,
        monto,
        fecha
    FROM Movimiento;
END;
CREATE PROCEDURE sp_TipoMovimiento_ObtenerPorId (
    @idTipoMovimiento UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Tipo_Movimiento WHERE idtipomovimiento = @idTipoMovimiento;
END;
CREATE PROCEDURE sp_Cuenta_ObtenerPorId (
    @idCuenta UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Cuenta WHERE idcuenta = @idCuenta;
END;
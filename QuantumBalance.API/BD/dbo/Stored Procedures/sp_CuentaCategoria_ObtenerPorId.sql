CREATE PROCEDURE sp_CuentaCategoria_ObtenerPorId (
    @idCategoria UNIQUEIDENTIFIER,
    @idCuenta UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM CuentaCategoria 
    WHERE idcategoria = @idCategoria AND idcuenta = @idCuenta;
END;
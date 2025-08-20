CREATE PROCEDURE sp_CuentaCategoria_Eliminar
    @idcategoria UNIQUEIDENTIFIER,
    @idcuenta UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM CuentaCategoria
    WHERE idcategoria = @idcategoria
      AND idcuenta = @idcuenta;
END;
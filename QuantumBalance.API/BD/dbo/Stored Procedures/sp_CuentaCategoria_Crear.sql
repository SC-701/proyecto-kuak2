CREATE PROCEDURE sp_CuentaCategoria_Crear
    @idcategoria UNIQUEIDENTIFIER,
    @idcuenta UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO CuentaCategoria (idcategoria, idcuenta)
    VALUES (@idcategoria, @idcuenta);
END;
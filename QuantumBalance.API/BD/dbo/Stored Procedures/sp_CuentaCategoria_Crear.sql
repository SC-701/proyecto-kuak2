CREATE PROCEDURE [dbo].[sp_CuentaCategoria_Crear]
    @idcategoria UNIQUEIDENTIFIER,
    @idcuenta UNIQUEIDENTIFIER
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM CuentaCategoria
        WHERE idcategoria = @idcategoria
          AND idcuenta = @idcuenta
    )
    BEGIN
        INSERT INTO CuentaCategoria (idcategoria, idcuenta)
        VALUES (@idcategoria, @idcuenta);
    END
END;
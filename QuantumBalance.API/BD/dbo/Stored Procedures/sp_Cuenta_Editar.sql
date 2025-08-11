CREATE PROCEDURE sp_Cuenta_Editar
    @idCuenta UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255),
    @tipo VARCHAR(255)
AS
BEGIN
    UPDATE Cuenta
    SET nombre = @nombre,
        descripcion = @descripcion,
        tipo = @tipo
    WHERE idcuenta = @idCuenta;
END;
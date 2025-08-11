CREATE PROCEDURE sp_Cuenta_Crear
    @idCuenta UNIQUEIDENTIFIER,
    @idUsuario UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255),
    @tipo VARCHAR(255)
AS
BEGIN
    INSERT INTO Cuenta (idcuenta, idusuario, nombre, descripcion, tipo)
    VALUES (@idCuenta, @idUsuario, @nombre, @descripcion, @tipo);

    SELECT @idCuenta;
END;
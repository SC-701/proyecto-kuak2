CREATE PROCEDURE sp_Usuario_Crear
    @idUsuario UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @primerApellido VARCHAR(255),
    @segundoApellido VARCHAR(255),
    @correo VARCHAR(255),
    @contrasena VARCHAR(255)
AS
BEGIN
    INSERT INTO Usuario (idusuario, nombre, primerapellido, segundoapellido, correo, contrasena)
    VALUES (@idUsuario, @nombre, @primerApellido, @segundoApellido, @correo, @contrasena);

    SELECT @idUsuario;
END;
CREATE PROCEDURE sp_Usuario_Editar
    @idUsuario UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @primerApellido VARCHAR(255),
    @segundoApellido VARCHAR(255),
    @correo VARCHAR(255),
    @contrasena VARCHAR(255)
AS
BEGIN
    UPDATE Usuario
    SET 
        nombre = @nombre,
        primerapellido = @primerApellido,
        segundoapellido = @segundoApellido,
        correo = @correo,
        contrasena = @contrasena
    WHERE idusuario = @idUsuario;
END;
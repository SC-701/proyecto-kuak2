CREATE PROCEDURE sp_Usuario_Eliminar
    @idUsuario UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Usuario
    WHERE idusuario = @idUsuario;
END;
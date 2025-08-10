CREATE PROCEDURE sp_Usuario_Mostrar
AS
BEGIN
    SELECT 
        idusuario AS IdUsuario, 
        nombre, 
        primerapellido AS PrimerApellido, 
        segundoapellido AS SegundoApellido, 
        correo, 
        contrasena
    FROM Usuario;
END;
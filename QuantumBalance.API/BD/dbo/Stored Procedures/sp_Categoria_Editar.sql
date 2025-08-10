CREATE PROCEDURE sp_Categoria_Editar
    @idCategoria UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255)
AS
BEGIN
    UPDATE Categoria
    SET nombre = @nombre,
        descripcion = @descripcion
    WHERE idcategoria = @idCategoria;
END;
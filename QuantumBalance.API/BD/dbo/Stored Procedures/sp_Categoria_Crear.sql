CREATE PROCEDURE sp_Categoria_Crear
    @idCategoria UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255)
AS
BEGIN
    INSERT INTO Categoria (idcategoria, nombre, descripcion)
    VALUES (@idCategoria, @nombre, @descripcion);

    SELECT @idCategoria;
END;
CREATE PROCEDURE sp_Categoria_Mostrar
AS
BEGIN
    SELECT idcategoria AS IdCategoria, nombre, descripcion
    FROM Categoria;
END;
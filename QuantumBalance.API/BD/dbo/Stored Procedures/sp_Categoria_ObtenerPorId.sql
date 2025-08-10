CREATE PROCEDURE sp_Categoria_ObtenerPorId (
    @idCategoria UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Categoria WHERE idcategoria = @idCategoria;
END;
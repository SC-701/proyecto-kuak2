
CREATE PROCEDURE [dbo].[sp_Cuenta_Editar] (@idCuenta UNIQUEIDENTIFIER, @idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @tipo NVARCHAR(50), @permitirSalarioNegativo BIT, @fechaUltimaModificacion DATETIME, @estado BIT)
AS
BEGIN
    UPDATE Cuenta SET idUsuario = @idUsuario,nombre = @nombre, descripcion = @descripcion, tipo = @tipo, permitirSalarioNegativo = @permitirSalarioNegativo, fechaUltimaModificacion = @fechaUltimaModificacion,
        estado = @estado WHERE idCuenta = @idCuenta;
	select @idCuenta;
END;
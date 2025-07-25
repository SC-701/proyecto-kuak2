CREATE PROCEDURE sp_Cuenta_Crear (@idCuenta UNIQUEIDENTIFIER, @idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @tipo NVARCHAR(50), @permitirSalarioNegativo BIT, @fechaCreacion DATETIME,
    @fechaUltimaModificacion DATETIME, @estado BIT)
AS
BEGIN
    INSERT INTO Cuenta VALUES (@idCuenta, @idUsuario, @nombre, @descripcion, @tipo, @permitirSalarioNegativo, @fechaCreacion, @fechaUltimaModificacion, @estado);
	select @idCuenta;
END;
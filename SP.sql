---------------------------
-- SP USUARIO --
---------------------------

-- Crear Usuario
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
GO

-- Mostrar todos los Usuarios
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
GO

-- Editar Usuario
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
GO

-- Eliminar Usuario
CREATE PROCEDURE sp_Usuario_Eliminar
    @idUsuario UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Usuario
    WHERE idusuario = @idUsuario;
END;
GO

-- Obtener Usuario por Id
CREATE PROCEDURE sp_Usuario_ObtenerPorId (
    @idUsuario UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Usuario WHERE idusuario = @idUsuario;
END;


---------------------------
-- SP CUENTA --
---------------------------

-- Crear Cuenta
CREATE PROCEDURE sp_Cuenta_Crear
    @idCuenta UNIQUEIDENTIFIER,
    @idUsuario UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255),
    @tipo VARCHAR(255)
AS
BEGIN
    INSERT INTO Cuenta (idcuenta, idusuario, nombre, descripcion, tipo)
    VALUES (@idCuenta, @idUsuario, @nombre, @descripcion, @tipo);

    SELECT @idCuenta;
END;
GO

-- Mostrar Cuentas
CREATE PROCEDURE sp_Cuenta_Mostrar
AS
BEGIN
    SELECT * FROM Cuenta;
END;
GO

-- Actualizar Cuenta
CREATE PROCEDURE sp_Cuenta_Editar
    @idCuenta UNIQUEIDENTIFIER,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255),
    @tipo VARCHAR(255)
AS
BEGIN
    UPDATE Cuenta
    SET nombre = @nombre,
        descripcion = @descripcion,
        tipo = @tipo
    WHERE idcuenta = @idCuenta;
END;
GO

-- Eliminar Cuenta
CREATE PROCEDURE sp_Cuenta_Eliminar
    @idCuenta UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Cuenta
    WHERE idcuenta = @idCuenta;
END;
GO

-- Obtener Cuenta por Id
CREATE PROCEDURE sp_Cuenta_ObtenerPorId (
    @idCuenta UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Cuenta WHERE idcuenta = @idCuenta;
END;


---------------------------
-- SP CATEGORIA --
---------------------------

-- Crear Categoria
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
GO

-- Mostrar Categorias
CREATE PROCEDURE sp_Categoria_Mostrar
AS
BEGIN
    SELECT idcategoria AS IdCategoria, nombre, descripcion
    FROM Categoria;
END;
GO

-- Editar Categoria
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
GO

-- Eliminar Categoria
CREATE PROCEDURE sp_Categoria_Eliminar
    @idCategoria UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Categoria
    WHERE idcategoria = @idCategoria;
END;
GO
    
-- Obtener Categoria por Id
CREATE PROCEDURE sp_Categoria_ObtenerPorId (
    @idCategoria UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Categoria WHERE idcategoria = @idCategoria;
END;
GO

---------------------------
-- SP MOVIMIENTO --
---------------------------

-- Crear Movimiento
CREATE PROCEDURE sp_Movimiento_Crear
    @idMovimiento UNIQUEIDENTIFIER,
    @idCuenta UNIQUEIDENTIFIER,
    @idCategoria UNIQUEIDENTIFIER,
    @idTipoMovimiento UNIQUEIDENTIFIER,
    @descripcion VARCHAR(255),
    @monto DECIMAL(18,2),
    @fecha DATETIME
AS
BEGIN
    INSERT INTO Movimiento 
        (idMovimiento, idCuenta, idCategoria, idTipoMovimiento, descripcion, monto, fecha)
    VALUES
        (@idMovimiento, @idCuenta, @idCategoria, @idTipoMovimiento, @descripcion, @monto, @fecha);

    SELECT @idMovimiento;
END;
GO

-- Mostrar todos los movimientos
CREATE PROCEDURE sp_Movimiento_Mostrar
AS
BEGIN
    SELECT 
        idMovimiento AS IdMovimiento,
        idCuenta AS IdCuenta,
        idCategoria AS IdCategoria,
        idTipoMovimiento AS IdTipoMovimiento,
        descripcion,
        monto,
        fecha
    FROM Movimiento;
END;
GO

-- Editar Movimiento
CREATE PROCEDURE sp_Movimiento_Editar
    @idMovimiento UNIQUEIDENTIFIER,
    @idCuenta UNIQUEIDENTIFIER,
    @idCategoria UNIQUEIDENTIFIER,
    @idTipoMovimiento UNIQUEIDENTIFIER,
    @descripcion VARCHAR(255),
    @monto DECIMAL(18,2),
    @fecha DATETIME
AS
BEGIN
    UPDATE Movimiento
    SET
        idCuenta = @idCuenta,
        idCategoria = @idCategoria,
        idTipoMovimiento = @idTipoMovimiento,
        descripcion = @descripcion,
        monto = @monto,
        fecha = @fecha
    WHERE idMovimiento = @idMovimiento;
END;
GO

-- Eliminar Movimiento
CREATE PROCEDURE sp_Movimiento_Eliminar
    @idMovimiento UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Movimiento
    WHERE idMovimiento = @idMovimiento;
END;
GO

--Obtener Movimiento por Id
CREATE PROCEDURE sp_Movimiento_ObtenerPorId (
    @idMovimiento UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Movimiento WHERE idMovimiento = @idMovimiento;
END;


---------------------------
-- SP CuentaCategoria --
---------------------------

-- Crear CuentaCategoria
CREATE PROCEDURE sp_CuentaCategoria_Crear
    @idcategoria UNIQUEIDENTIFIER,
    @idcuenta UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO CuentaCategoria (idcategoria, idcuenta)
    VALUES (@idcategoria, @idcuenta);
END;

-- Mostrar todos los CuentaCategoria
CREATE PROCEDURE sp_CuentaCategoria_Mostrar
AS
BEGIN
    SELECT * 
    FROM CuentaCategoria;
END;

---------------------------
-- SP Tipo_Movimiento --
---------------------------

--Obtener Tipo_Movimiento por Id
CREATE PROCEDURE sp_TipoMovimiento_ObtenerPorId (
    @idTipoMovimiento UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Tipo_Movimiento WHERE idtipomovimiento = @idTipoMovimiento;
END;




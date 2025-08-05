create database QuantumBalance
go

use QuantumBalance
go

CREATE TABLE Categoria (
    idcategoria UNIQUEIDENTIFIER PRIMARY KEY,
    nombre VARCHAR(255),
    descripcion VARCHAR(255)
);

CREATE TABLE Usuario (
    idusuario UNIQUEIDENTIFIER PRIMARY KEY,
    nombre VARCHAR(255),
    primerapellido VARCHAR(255),
    segundoapellido VARCHAR(255),
    correo VARCHAR(255),
    contrasena VARCHAR(255)
);

CREATE TABLE Tipo_Movimiento (
    idtipomovimiento UNIQUEIDENTIFIER PRIMARY KEY,
    nombre VARCHAR(255)
);

CREATE TABLE Cuenta (
    idcuenta UNIQUEIDENTIFIER PRIMARY KEY,
    idusuario UNIQUEIDENTIFIER,
    nombre VARCHAR(255),
    descripcion VARCHAR(255),
    tipo VARCHAR(255),
    FOREIGN KEY (idusuario) REFERENCES Usuario(idusuario)
);

CREATE TABLE Movimiento (
    idMovimiento UNIQUEIDENTIFIER PRIMARY KEY,
    idCuenta UNIQUEIDENTIFIER,
    idCategoria UNIQUEIDENTIFIER,
    idTipoMovimiento UNIQUEIDENTIFIER,
    descripcion VARCHAR(255),
    monto DECIMAL(18,2),
    fecha DATETIME,
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idcuenta),
    FOREIGN KEY (idCategoria) REFERENCES Categoria(idcategoria),
    FOREIGN KEY (idTipoMovimiento) REFERENCES Tipo_Movimiento(idtipomovimiento)
);

CREATE TABLE CuentaCategoria (
    idcategoria UNIQUEIDENTIFIER,
    idcuenta UNIQUEIDENTIFIER,
    PRIMARY KEY (idcategoria, idcuenta),
    FOREIGN KEY (idcategoria) REFERENCES Categoria(idcategoria),
    FOREIGN KEY (idcuenta) REFERENCES Cuenta(idcuenta)
);


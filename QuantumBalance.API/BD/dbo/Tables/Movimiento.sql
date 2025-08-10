CREATE TABLE [dbo].[Movimiento] (
    [idMovimiento]     UNIQUEIDENTIFIER NOT NULL,
    [idCuenta]         UNIQUEIDENTIFIER NULL,
    [idCategoria]      UNIQUEIDENTIFIER NULL,
    [idTipoMovimiento] UNIQUEIDENTIFIER NULL,
    [descripcion]      VARCHAR (255)    NULL,
    [monto]            DECIMAL (18, 2)  NULL,
    [fecha]            DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([idMovimiento] ASC),
    FOREIGN KEY ([idCategoria]) REFERENCES [dbo].[Categoria] ([idcategoria]),
    FOREIGN KEY ([idCuenta]) REFERENCES [dbo].[Cuenta] ([idcuenta]),
    FOREIGN KEY ([idTipoMovimiento]) REFERENCES [dbo].[Tipo_Movimiento] ([idtipomovimiento])
);


CREATE TABLE [dbo].[Cuenta] (
    [idCuenta]                UNIQUEIDENTIFIER NOT NULL,
    [idUsuario]               UNIQUEIDENTIFIER NULL,
    [nombre]                  NVARCHAR (100)   NULL,
    [descripcion]             NVARCHAR (MAX)   NULL,
    [tipo]                    NVARCHAR (50)    NULL,
    [permitirSalarioNegativo] BIT              NULL,
    [fechaCreacion]           DATETIME         NULL,
    [fechaUltimaModificacion] DATETIME         NULL,
    [estado]                  BIT              NULL,
    PRIMARY KEY CLUSTERED ([idCuenta] ASC),
    FOREIGN KEY ([idUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario])
);


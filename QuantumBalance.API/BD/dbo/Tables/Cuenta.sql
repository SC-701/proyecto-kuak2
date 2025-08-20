CREATE TABLE [dbo].[Cuenta] (
    [idcuenta]    UNIQUEIDENTIFIER NOT NULL,
    [idusuario]   UNIQUEIDENTIFIER NULL,
    [nombre]      VARCHAR (255)    NULL,
    [descripcion] VARCHAR (255)    NULL,
    [tipo]        VARCHAR (255)    NULL,
    PRIMARY KEY CLUSTERED ([idcuenta] ASC),
    CONSTRAINT [FK_Cuenta_Usuarios] FOREIGN KEY ([idusuario]) REFERENCES [dbo].[Usuarios] ([Id])
);


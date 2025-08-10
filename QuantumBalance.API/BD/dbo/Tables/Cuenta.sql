CREATE TABLE [dbo].[Cuenta] (
    [idcuenta]    UNIQUEIDENTIFIER NOT NULL,
    [idusuario]   UNIQUEIDENTIFIER NULL,
    [nombre]      VARCHAR (255)    NULL,
    [descripcion] VARCHAR (255)    NULL,
    [tipo]        VARCHAR (255)    NULL,
    PRIMARY KEY CLUSTERED ([idcuenta] ASC),
    FOREIGN KEY ([idusuario]) REFERENCES [dbo].[Usuario] ([idusuario])
);


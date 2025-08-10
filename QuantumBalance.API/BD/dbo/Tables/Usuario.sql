CREATE TABLE [dbo].[Usuario] (
    [idusuario]       UNIQUEIDENTIFIER NOT NULL,
    [nombre]          VARCHAR (255)    NULL,
    [primerapellido]  VARCHAR (255)    NULL,
    [segundoapellido] VARCHAR (255)    NULL,
    [correo]          VARCHAR (255)    NULL,
    [contrasena]      VARCHAR (255)    NULL,
    PRIMARY KEY CLUSTERED ([idusuario] ASC)
);


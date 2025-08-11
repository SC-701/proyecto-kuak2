CREATE TABLE [dbo].[Categoria] (
    [idcategoria] UNIQUEIDENTIFIER NOT NULL,
    [nombre]      VARCHAR (255)    NULL,
    [descripcion] VARCHAR (255)    NULL,
    PRIMARY KEY CLUSTERED ([idcategoria] ASC)
);


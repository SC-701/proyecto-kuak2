﻿CREATE PROCEDURE sp_Cuenta_Eliminar
    @idCuenta UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Cuenta
    WHERE idcuenta = @idCuenta;
END;
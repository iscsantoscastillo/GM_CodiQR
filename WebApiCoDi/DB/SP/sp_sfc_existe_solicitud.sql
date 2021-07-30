USE [SoftCredito]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- CREATE PROCEDURE sp_sfc_existe_solicitud -- En caso de que no exista
ALTER PROCEDURE sp_sfc_existe_solicitud
    @clave_solicitud nvarchar(20) 
AS 
BEGIN
	DECLARE @existe int = 1; -- 0 = No existe, 1 = sí existe
        IF NOT EXISTS
        (
            SELECT
                1
            FROM SoftCredito..sfc_ventas_softcredito 
            WHERE 
                vs_status = 1 AND
                clave_solicitud = @clave_solicitud	
        )
        BEGIN
            SET @existe= 0
        END

		SELECT @existe AS existe;
END
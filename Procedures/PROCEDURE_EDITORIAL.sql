
USE Biblioteca
go

Create or alter procedure sp_ListarPais
as
begin
	select CodigoPais, NombrePais from dbo.Pais
end
go

CREATE OR ALTER PROCEDURE sp_ListarEditoriales
AS
BEGIN
    SELECT E.CodigoEditorial, E.NombreEditorial, E.Direccion, E.Email, 
           E.CodigoPais, P.NombrePais
    FROM Editorial E
    INNER JOIN Pais P ON E.CodigoPais = P.CodigoPais
END
GO


Create or alter procedure sp_InsertarEditorial
@CodigoEditorial char(5),
@NombreEditorial varchar(100),
@Direccion varchar(255),
@Email varchar(100),
@CodigoPais char(3)
as
begin
	INSERT INTO Editorial(CodigoEditorial, NombreEditorial, Direccion, Email,CodigoPais)
	Values(@CodigoEditorial,
			@NombreEditorial,
			@Direccion,
			@Email,
			@CodigoPais)
end
go

Create or alter procedure sp_ActualizarEditoral
@CodigoEditorial char(5),
@NombreEditorial varchar(100),
@Direccion varchar(255),
@Email varchar(100),
@CodigoPais char(3)
as
begin
	UPDATE Editorial
	SET NombreEditorial = @NombreEditorial,
		Direccion = @Direccion,
		Email = @Email,
		CodigoPais = @CodigoPais
	where CodigoEditorial = @CodigoEditorial
end
go

Create or alter procedure sp_EliminarEditorial
@CodigoEditorial char(5)
as
begin
		Delete from Editorial where CodigoEditorial = @CodigoEditorial
end
go


CREATE OR ALTER PROCEDURE sp_BuscarEditorialPorNombre
    @Nombre varchar(100)
AS
BEGIN
    SELECT E.CodigoEditorial, E.NombreEditorial, E.Direccion, E.Email, 
           E.CodigoPais, P.NombrePais
    FROM Editorial E
    INNER JOIN Pais P ON E.CodigoPais = P.CodigoPais
    WHERE E.NombreEditorial LIKE '%' + @Nombre + '%'
END
GO
go
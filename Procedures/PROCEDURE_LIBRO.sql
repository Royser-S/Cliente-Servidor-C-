USE Biblioteca
GO

CREATE OR ALTER PROCEDURE sp_ListarEditoriales
AS
BEGIN
    SELECT E.CodigoEditorial, E.NombreEditorial, E.Direccion, E.Email, 
           E.CodigoPais, P.NombrePais
    FROM Editorial E
    INNER JOIN Pais P ON E.CodigoPais = P.CodigoPais
END
GO

-- 1. LISTAR LIBROS (CON JOIN A EDITORIAL)
-- Este trae el nombre de la editorial en vez de solo el código
CREATE OR ALTER PROCEDURE sp_ListarLibros
AS
BEGIN
    SELECT L.CodigoLibro, L.TituloLibro, L.Autor, L.Genero, 
           L.CodigoEditorial, E.NombreEditorial
    FROM Libro L
    INNER JOIN Editorial E ON L.CodigoEditorial = E.CodigoEditorial
END
GO

CREATE OR ALTER PROCEDURE sp_InsertarLibro
	@CodigoLibro char(4),
    @TituloLibro varchar(200),
    @Autor varchar(150),
    @Genero varchar(50),
    @CodigoEditorial char(5) -- Debe coincidir con el tipo de dato de tu tabla Editorial
AS
BEGIN
    INSERT INTO Libro(CodigoLibro,TituloLibro, Autor, Genero, CodigoEditorial)
    VALUES (@CodigoLibro,@TituloLibro, @Autor, @Genero, @CodigoEditorial)
END
GO

-- 3. ACTUALIZAR LIBRO
CREATE OR ALTER PROCEDURE sp_ActualizarLibro
	@CodigoLibro char(4),
    @TituloLibro varchar(100),
    @Autor varchar(100),
    @Genero varchar(50),
    @CodigoEditorial char(5)
AS
BEGIN
    UPDATE Libro
    SET TituloLibro = @TituloLibro,
        Autor = @Autor,
        Genero = @Genero,
        CodigoEditorial = @CodigoEditorial
    WHERE CodigoLibro = @CodigoLibro
END
GO

-- 4. ELIMINAR LIBRO
CREATE OR ALTER PROCEDURE sp_EliminarLibro
	@CodigoLibro char(4)
AS
BEGIN
    DELETE FROM Libro WHERE CodigoLibro = @CodigoLibro
END
GO

-- 5. OBTENER UN SOLO LIBRO (Para cuando le des clic a Editar)
CREATE OR ALTER PROCEDURE sp_ObtenerLibro
	@CodigoLibro char(4)
AS
BEGIN
    SELECT CodigoLibro, TituloLibro, Autor, Genero, CodigoEditorial
    FROM Libro
    WHERE CodigoLibro = @CodigoLibro
END
GO

-- 6. FILTRO POR AUTOR (Para la barra de búsqueda)
CREATE OR ALTER PROCEDURE sp_ListarLibrosPorAutor
    @Autor varchar(150)
AS
BEGIN
    SELECT L.CodigoLibro, L.TituloLibro, L.Autor, L.Genero, 
           L.CodigoEditorial, E.NombreEditorial
    FROM Libro L
    INNER JOIN Editorial E ON L.CodigoEditorial = E.CodigoEditorial
    WHERE L.Autor LIKE '%' + @Autor + '%'
END
GO

-- 7. (EXTRA) FILTRO POR EDITORIAL (Para el Combo)
-- Este sirve si en el Index quieres filtrar libros seleccionando una editorial
CREATE OR ALTER PROCEDURE sp_ListarLibrosPorEditorial
    @CodigoEditorial char(5)
AS
BEGIN
    SELECT L.CodigoLibro, L.TituloLibro, L.Autor, L.Genero, 
           L.CodigoEditorial, E.NombreEditorial
    FROM Libro L
    INNER JOIN Editorial E ON L.CodigoEditorial = E.CodigoEditorial
    WHERE L.CodigoEditorial = @CodigoEditorial
END
GO
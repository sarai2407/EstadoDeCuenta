CREATE PROCEDURE CrearUsuario
    @Nombre NVARCHAR(MAX),
    @Apellidos NVARCHAR(MAX),
    @Telefono NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @Dui NVARCHAR(MAX),
    @FechaRegistro DATETIME2(7)
AS
BEGIN

    INSERT INTO Usuario (Nombre, Apellidos, Telefono, Email, Dui, FechaRegistro)
    VALUES (@Nombre, @Apellidos, @Telefono, @Email, @Dui, @FechaRegistro);

    -- Devuelve el ID del nuevo usuario insertado
    SELECT SCOPE_IDENTITY() AS idUser;
END;

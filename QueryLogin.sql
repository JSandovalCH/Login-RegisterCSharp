-- CREANDO BASE DE DATOS
CREATE DATABASE LoginNCapasBD

-- USANDO LA BASE DE DATOS
USE LoginNCapasBD

-- CREANDO TABLAS
CREATE TABLE Users(
ID INT IDENTITY(1,1) PRIMARY KEY,
Name NVARCHAR(30),
Lastname NVARCHAR(30),
Username NVARCHAR(30),
Pass NVARCHAR(30)
);

CREATE TABLE Productos 
(
Id INT IDENTITY (1,1) PRIMARY KEY,
Nombre NVARCHAR(100),
Descripcion NVARCHAR(100),
Marca NVARCHAR(100),
Precio FLOAT,
Stock INT
)

-- INSERTAR VALORES
INSERT INTO Productos VALUES ('Gaseosa','3 litros','marcacola',7.5,24),
							 ('Chocolate','Tableta 100 gramos','iberica',12.5,36)

-- VER TABLA
SELECT * FROM Users

--PROCEDIMIENTO ALMACENADO
CREATE PROC SP_CreateUser
@Name NVARCHAR(30),
@Lastname NVARCHAR(30),
@Username NVARCHAR(30),
@Pass NVARCHAR(30)
AS
INSERT INTO Users VALUES(@Name, @Lastname, @Username, @Pass)

--------------------------MOSTRAR 
CREATE PROC MostrarProductos
AS
SELECT * FROM Productos

--------------------------INSERTAR 
CREATE PROC InsetarProductos
@nombre NVARCHAR(100),
@descrip NVARCHAR(100),
@marca NVARCHAR(100),
@precio FLOAT,
@stock INT
AS
INSERT INTO Productos VALUES(@nombre,@descrip,@marca,@precio,@stock)

------------------------ELIMINAR
CREATE PROC EliminarProducto
@idpro INT
AS
DELETE FROM Productos WHERE Id=@idpro

------------------EDITAR
CREATE PROC EditarProductos
@nombre NVARCHAR(100),
@descrip NVARCHAR(100),
@marca NVARCHAR(100),
@precio FLOAT,
@stock INT,
@id INT
AS
update Productos SET Nombre=@nombre, Descripcion=@descrip, Marca=@marca, Precio=@precio, Stock=@stock WHERE Id=@id
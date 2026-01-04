-- Script para crear las tablas de la base de datos del restaurante
-- Base de datos: RestauranteDB

CREATE DATABASE RestauranteDB;
GO

USE RestauranteDB;
GO

-- Tabla Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(255),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

-- Tabla Mesas
CREATE TABLE Mesas (
    MesaID INT PRIMARY KEY IDENTITY(1,1),
    Capacidad INT NOT NULL,
    Ubicacion NVARCHAR(50),
    Ocupada BIT DEFAULT 0
);

-- Tabla MenuItems
CREATE TABLE MenuItems (
    MenuItemID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(10,2) NOT NULL,
    Categoria NVARCHAR(50),
    Disponible BIT DEFAULT 1
);

-- Tabla Pedidos
CREATE TABLE Pedidos (
    PedidoID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL,
    MesaID INT,
    FechaPedido DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    Estado NVARCHAR(50) DEFAULT 'Pendiente',
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    FOREIGN KEY (MesaID) REFERENCES Mesas(MesaID)
);

-- Tabla DetallesPedido
CREATE TABLE DetallesPedido (
    DetalleID INT PRIMARY KEY IDENTITY(1,1),
    PedidoID INT NOT NULL,
    MenuItemID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID),
    FOREIGN KEY (MenuItemID) REFERENCES MenuItems(MenuItemID)
);

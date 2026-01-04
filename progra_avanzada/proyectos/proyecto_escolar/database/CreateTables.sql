-- Script para crear las tablas de la base de datos escolar
-- Base de datos: EscuelaDB

CREATE DATABASE EscuelaDB;
GO

USE EscuelaDB;
GO

-- Tabla Estudiantes
CREATE TABLE Estudiantes (
    EstudianteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(255),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

-- Tabla Profesores
CREATE TABLE Profesores (
    ProfesorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Telefono NVARCHAR(20),
    Especialidad NVARCHAR(100),
    FechaContratacion DATE NOT NULL
);

-- Tabla Cursos
CREATE TABLE Cursos (
    CursoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Creditos INT NOT NULL,
    ProfesorID INT,
    FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID)
);

-- Tabla Matriculas
CREATE TABLE Matriculas (
    MatriculaID INT PRIMARY KEY IDENTITY(1,1),
    EstudianteID INT NOT NULL,
    CursoID INT NOT NULL,
    FechaMatricula DATETIME DEFAULT GETDATE(),
    Calificacion DECIMAL(5,2) NULL,
    FOREIGN KEY (EstudianteID) REFERENCES Estudiantes(EstudianteID),
    FOREIGN KEY (CursoID) REFERENCES Cursos(CursoID),
    UNIQUE(EstudianteID, CursoID)
);

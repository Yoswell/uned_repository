-- Script para insertar datos de ejemplo en la base de datos escolar
USE EscuelaDB;
GO

-- Insertar profesores
INSERT INTO Profesores (Nombre, Apellido, Email, Telefono, Especialidad, FechaContratacion) VALUES
('Juan', 'Pérez', 'juan.perez@escuela.com', '8888-1111', 'Matemáticas', '2020-01-15'),
('María', 'García', 'maria.garcia@escuela.com', '8888-2222', 'Programación', '2019-08-20'),
('Carlos', 'Rodríguez', 'carlos.rodriguez@escuela.com', '8888-3333', 'Física', '2021-03-10');

-- Insertar cursos
INSERT INTO Cursos (Nombre, Descripcion, Creditos, ProfesorID) VALUES
('Álgebra', 'Curso básico de álgebra', 3, 1),
('Programación en C#', 'Introducción a la programación orientada a objetos con C#', 4, 2),
('Física General', 'Principios básicos de física', 3, 3),
('Estructuras de Datos', 'Algoritmos y estructuras de datos avanzadas', 4, 2);

-- Insertar estudiantes
INSERT INTO Estudiantes (Nombre, Apellido, FechaNacimiento, Email, Telefono, Direccion) VALUES
('Ana', 'López', '2000-05-15', 'ana.lopez@estudiante.com', '7777-1111', 'Calle 1, Ciudad'),
('Pedro', 'Martínez', '1999-12-03', 'pedro.martinez@estudiante.com', '7777-2222', 'Avenida 2, Ciudad'),
('Sofia', 'Hernández', '2001-08-22', 'sofia.hernandez@estudiante.com', '7777-3333', 'Plaza 3, Ciudad'),
('Luis', 'González', '2000-11-30', 'luis.gonzalez@estudiante.com', '7777-4444', 'Boulevard 4, Ciudad');

-- Insertar matriculas
INSERT INTO Matriculas (EstudianteID, CursoID, Calificacion) VALUES
(1, 1, 85.5),
(1, 2, 92.0),
(2, 2, 88.5),
(2, 3, 76.0),
(3, 1, 90.0),
(3, 4, 87.5),
(4, 3, 81.0),
(4, 4, 89.0);

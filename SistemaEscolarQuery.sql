CREATE DATABASE EscuelaDB

CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contraseña NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) -- Ej: "Administrador", "Profesor"
);

CREATE TABLE Estudiantes (
    IdEstudiante INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Matricula NVARCHAR(20) UNIQUE,
    FechaNacimiento DATE,
    Curso NVARCHAR(50)
);

CREATE TABLE Profesores (
    IdProfesor INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Especialidad NVARCHAR(50),
    Email NVARCHAR(100)
);

CREATE TABLE Cursos (
    IdCurso INT PRIMARY KEY IDENTITY(1,1),
    NombreCurso NVARCHAR(100),
    IdProfesor INT FOREIGN KEY REFERENCES Profesores(IdProfesor)
);

CREATE TABLE Inscripciones (
    IdInscripcion INT PRIMARY KEY IDENTITY(1,1),
    IdEstudiante INT FOREIGN KEY REFERENCES Estudiantes(IdEstudiante),
    IdCurso INT FOREIGN KEY REFERENCES Cursos(IdCurso),
    FechaInscripcion DATE
);


--Data para pruebas
INSERT INTO Usuarios (NombreUsuario, Contraseña, Rol) VALUES
('admin', 'admin123', 'Administrador'),
('profesor1', 'clave123', 'Profesor'),
('usuario2', 'pass456', 'Profesor');

INSERT INTO Estudiantes (Nombre, Matricula, FechaNacimiento, Curso) VALUES
('Ana Martínez', 'MAT001', '2008-05-12', '6to Grado'),
('Luis Gómez', 'MAT002', '2007-11-23', '7mo Grado'),
('Carla Reyes', 'MAT003', '2009-02-15', '6to Grado'),
('Pedro Núñez', 'MAT004', '2008-08-30', '7mo Grado');


INSERT INTO Profesores (Nombre, Especialidad, Email) VALUES
('María López', 'Matemáticas', 'mlopez@escuela.edu'),
('José Ramírez', 'Lengua Española', 'jramirez@escuela.edu'),
('Laura Peña', 'Ciencias Naturales', 'lpena@escuela.edu');

INSERT INTO Cursos (NombreCurso, IdProfesor) VALUES
('Matemáticas 6to', 1),
('Lengua Española 7mo', 2),
('Ciencias Naturales 6to', 3);


INSERT INTO Inscripciones (IdEstudiante, IdCurso, FechaInscripcion) VALUES
(1, 1, '2025-08-15'),
(2, 2, '2025-08-16'),
(3, 1, '2025-08-17'),
(4, 2, '2025-08-18'),
(1, 3, '2025-08-19');


SELECT * FROM Usuarios;
SELECT * FROM Estudiantes;
SELECT * FROM Profesores;
SELECT * FROM Cursos;
SELECT * FROM Inscripciones;

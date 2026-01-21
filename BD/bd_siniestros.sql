CREATE DATABASE siniestros_viales;

USE siniestros_viales;
--Tablas
CREATE TABLE departamentos (
    departamentos_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE ciudades (
    ciudades_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    departamentos_id INT NOT NULL,
    CONSTRAINT FK_Ciudad_Departamento
        FOREIGN KEY (departamentos_id) REFERENCES departamentos(departamentos_id)
);

CREATE TABLE tipos_siniestro (
    tipos_siniestro_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Siniestros (
    Siniestros_id UNIQUEIDENTIFIER PRIMARY KEY,
    fechahora DATETIME2 NOT NULL,
    departamentos_id INT NOT NULL,
    ciudades_id INT NOT NULL,
    tipos_siniestro_id INT NOT NULL,
    vehiculos_involucrados INT NOT NULL,
    numero_victimas INT NOT NULL,
    descripcion NVARCHAR(500),

    CONSTRAINT FK_Siniestro_Departamento
        FOREIGN KEY (departamentos_id) REFERENCES departamentos(departamentos_id),

    CONSTRAINT FK_Siniestro_Ciudad
        FOREIGN KEY (ciudades_id) REFERENCES ciudades(ciudades_id),

    CONSTRAINT FK_Siniestro_Tipo
        FOREIGN KEY (tipos_siniestro_id) REFERENCES tipos_siniestro(tipos_siniestro_id)
);

--Indices
CREATE INDEX IX_Siniestros_FechaHora
ON Siniestros(FechaHora);

CREATE INDEX IX_Siniestros_Departamento_Fecha
ON Siniestros(departamentos_id, FechaHora);


--Inserts tablas tipo
INSERT INTO tipos_siniestro (nombre) VALUES
('Choque'),
('Atropello'),
('Volcamiento'),
('Colisión múltiple'),
('Otro');

INSERT INTO departamentos (nombre) VALUES
('Amazonas'),
('Antioquia'),
('Arauca'),
('Atlántico'),
('Bolívar'),
('Boyacá'),
('Caldas'),
('Caquetá'),
('Casanare'),
('Cauca'),
('Cesar'),
('Chocó'),
('Córdoba'),
('Cundinamarca'),
('Guainía'),
('Guaviare'),
('Huila'),
('La Guajira'),
('Magdalena'),
('Meta'),
('Nariño'),
('Norte de Santander'),
('Putumayo'),
('Quindío'),
('Risaralda'),
('San Andrés y Providencia'),
('Santander'),
('Sucre'),
('Tolima'),
('Valle del Cauca'),
('Vaupés'),
('Vichada'),
('Bogotá D.C.');

--Algunas ciudades
-- Antioquia
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Medellín', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia')),
('Envigado', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia')),
('Itagüí', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia')),
('Bello', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia'));

--Cundinamarca
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Soacha', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca')),
('Chía', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca')),
('Zipaquirá', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca')),
('Facatativá', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca'));

--Bogotá D.C.
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Bogotá', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Bogotá D.C.'));

--Valle del Cauca
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Cali', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Valle del Cauca')),
('Palmira', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Valle del Cauca')),
('Buenaventura', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Valle del Cauca'));

--Atlántico
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Barranquilla', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Atlántico')),
('Soledad', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Atlántico'));

--Santander
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Bucaramanga', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Santander')),
('Floridablanca', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Santander'));

--Bolívar
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Cartagena', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Bolívar')),
('Magangué', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Bolívar'));

--Nariño
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Pasto', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Nariño')),
('Ipiales', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Nariño'));

--prueba
SELECT d.Nombre AS Departamento, c.Nombre AS Ciudad
FROM Ciudades c
JOIN Departamentos d ON c.departamentos_id = d.departamentos_id
ORDER BY d.Nombre;


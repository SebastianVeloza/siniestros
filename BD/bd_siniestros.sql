IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SiniestrosDB')
BEGIN
    CREATE DATABASE SiniestrosDB;
END
GO

USE SiniestrosDB;
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

CREATE TABLE historico_refresh_token (
    historico_refresh_token_id INT IDENTITY(1,1) PRIMARY KEY,
    token NVARCHAR(MAX) NOT NULL,
	refresh_token NVARCHAR(500) NOT NULL,
	fecha_creacion DATETIME NOT NULL,
	fecha_expiracion DATETIME NOT NULL,
	activo AS ( CASE WHEN fecha_expiracion< GETDATE() THEN CONVERT(BIT,(0)) ELSE CONVERT(BIT,(1)) END),
);
CREATE TABLE Logs_Siniestros (
    Logs_Siniestros_id INT IDENTITY(1,1) PRIMARY KEY,
    fechahora DATETIME2 NOT NULL,
    accion NVARCHAR(20) NOT NULL,
    envio NVARCHAR(MAX) NOT NULL,
    tabla NVARCHAR(100) NOT NULL,
    
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
('Colisi�n m�ltiple'),
('Otro');

INSERT INTO departamentos (nombre) VALUES
('Amazonas'),
('Antioquia'),
('Arauca'),
('Atl�ntico'),
('Bol�var'),
('Boyac�'),
('Caldas'),
('Caquet�'),
('Casanare'),
('Cauca'),
('Cesar'),
('Choc�'),
('C�rdoba'),
('Cundinamarca'),
('Guain�a'),
('Guaviare'),
('Huila'),
('La Guajira'),
('Magdalena'),
('Meta'),
('Nari�o'),
('Norte de Santander'),
('Putumayo'),
('Quind�o'),
('Risaralda'),
('San Andr�s y Providencia'),
('Santander'),
('Sucre'),
('Tolima'),
('Valle del Cauca'),
('Vaup�s'),
('Vichada'),
('Bogot� D.C.');

--Algunas ciudades
-- Antioquia
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Medell�n', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia')),
('Envigado', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia')),
('Itag��', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia')),
('Bello', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Antioquia'));

--Cundinamarca
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Soacha', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca')),
('Ch�a', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca')),
('Zipaquir�', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca')),
('Facatativ�', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Cundinamarca'));

--Bogot� D.C.
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Bogot�', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Bogot� D.C.'));

--Valle del Cauca
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Cali', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Valle del Cauca')),
('Palmira', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Valle del Cauca')),
('Buenaventura', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Valle del Cauca'));

--Atl�ntico
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Barranquilla', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Atl�ntico')),
('Soledad', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Atl�ntico'));

--Santander
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Bucaramanga', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Santander')),
('Floridablanca', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Santander'));

--Bol�var
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Cartagena', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Bol�var')),
('Magangu�', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Bol�var'));

--Nari�o
INSERT INTO ciudades (Nombre, departamentos_id)
VALUES
('Pasto', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Nari�o')),
('Ipiales', (SELECT departamentos_id FROM departamentos WHERE Nombre = 'Nari�o'));

--prueba
SELECT d.Nombre AS Departamento, c.Nombre AS Ciudad
FROM Ciudades c
JOIN Departamentos d ON c.departamentos_id = d.departamentos_id
ORDER BY d.Nombre;


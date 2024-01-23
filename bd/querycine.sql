-- Crear la base de datos
CREATE DATABASE Bd_Cine;
GO

-- Usar la base de datos
USE Bd_Cine;
GO

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
);
GO

-- Insertar roles en la tabla de roles
INSERT INTO Roles (Nombre) VALUES ('Administrador General');
INSERT INTO Roles (Nombre) VALUES ('Administrador');
INSERT INTO Roles (Nombre) VALUES ('Empleado');
INSERT INTO Roles (Nombre) VALUES ('Cliente');
GO

update Usuarios set RolId = 1 where Id = 3;

GO

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(100) NOT NULL,
    NombreUsuario NVARCHAR(50) UNIQUE NOT NULL,
    Correo NVARCHAR(100) UNIQUE NOT NULL,
    Contrasena NVARCHAR(100) NOT NULL,
    TokenVerificacionCorreo NVARCHAR(255),
    TokenRecuperacionContrasena NVARCHAR(255),
    FechaTokenRecuperacionContrasena DATETIME,
    CuentaVerificada BIT DEFAULT 0,
    RolId INT FOREIGN KEY REFERENCES Roles(Id) DEFAULT 4 -- Por defecto Cliente
);
GO

CREATE PROCEDURE sp_registrar
    @NombreCompleto NVARCHAR(100),
    @NombreUsuario NVARCHAR(50),
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(100),
    @TokenVerificacionCorreo NVARCHAR(255),
    @CuentaVerificada BIT,
    @RolId INT
AS 
BEGIN
    INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Correo, Contrasena, TokenVerificacionCorreo, CuentaVerificada, RolId) 
    VALUES(@NombreCompleto, @NombreUsuario, @Correo, @Contrasena, @TokenVerificacionCorreo, @CuentaVerificada, @RolId)

    -- Devuelve el ID del usuario recién insertado
    SELECT SCOPE_IDENTITY() AS NewUserId
END

CREATE PROCEDURE sp_validar
@TokenVerificacionCorreo NVARCHAR(255)
AS BEGIN
DECLARE @Correo NVARCHAR(100)
SET @Correo = (SELECT Correo from dbo.Usuarios where TokenVerificacionCorreo=@TokenVerificacionCorreo)
Update dbo.Usuarios set CuentaVerificada=1 Where TokenVerificacionCorreo=@TokenVerificacionCorreo
Update dbo.Usuarios set TokenVerificacionCorreo=null where Correo=@Correo
END

-- Creación de la tabla TC_Estatus_Roles
CREATE TABLE TC_Estatus_Roles (
    id_tc_estatusRoles INT PRIMARY KEY IDENTITY(1,1),
    descripcion NVARCHAR(50) NOT NULL
);

-- Inserciones para TC_Estatus_Roles
INSERT INTO TC_Estatus_Roles (descripcion) VALUES ('activo'), ('inactivo');

-- Creación de la tabla TD_RolesUsuarios
CREATE TABLE TD_RolesUsuarios (
    id_roles_td INT PRIMARY KEY IDENTITY(1,1),
    id_usuario INT NOT NULL,
    id_role INT NOT NULL,
    id_estatus INT NOT NULL,
	fecha	DATETIME,
	usuario  NVARCHAR(100)
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(Id), -- Asumiendo que el nombre de la tabla de usuarios es Usuarios y su campo principal es Id
    FOREIGN KEY (id_role) REFERENCES Roles(Id), -- Asumiendo que el nombre de la tabla de roles es Roles y su campo principal es Id
    FOREIGN KEY (id_estatus) REFERENCES TC_Estatus_Roles(id_tc_estatusRoles)
);

-- Tabla Maestra De Pelicula 
CREATE TABLE TM_Pelicula (
 Id INT PRIMARY KEY IDENTITY(1,1),
 titulo  NVARCHAR(255) NOT NULL,
 duracion NVARCHAR(30) NOT NULL,
 clasificacion NVARCHAR(15) NOT NULL,
 ruta_archivo NVARCHAR(4000) NOT NULL,
 director NVARCHAR(100) ,
 actor NVARCHAR(100) ,
 descripcion NVARCHAR(255) ,
 año INT
);
GO

-- Tabla Maestra De Sala
CREATE TABLE TM_Sala (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Numero_sala INT NOT NULL,
	Capacidad_asiento INT NOT NULL
);
GO

-- Tabla Detalle Funcion
CREATE TABLE TD_Funcion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
	HoraInicio TIME NOT NULL,
	HoraFin TIME NOT NULL,
	IdPeli INT NOT NULL FOREIGN KEY REFERENCES TM_Pelicula(Id),
	IdSala INT NOT NULL FOREIGN KEY REFERENCES TM_Sala(Id)
);
GO

CREATE TABLE Venta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaVenta DATETIME NOT NULL,
	Cantidad INT NOT NULL,
	Total DECIMAL(10, 2) NOT NULL
);
GO

CREATE TABLE Asiento (
 Id INT PRIMARY KEY IDENTITY(1,1),
 Fila INT ,
 Num_Asiento INT,
 IdSala INT NOT NULL FOREIGN KEY REFERENCES TM_Sala(Id)
);
GO

CREATE TABLE Boleto (
 Id INT PRIMARY KEY IDENTITY(1,1),
 costo DECIMAL(10, 2) NOT NULL,
 IdAsiento INT NOT NULL FOREIGN KEY REFERENCES Asiento(Id),
 IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
 IdFuncion INT NOT NULL FOREIGN KEY REFERENCES TD_Funcion(Id),
 IdVenta INT NOT NULL FOREIGN KEY REFERENCES Venta(Id)
);
GO


CREATE TABLE TC_TARJETA(
Id INT PRIMARY KEY IDENTITY (1,1),
nombre  NVARCHAR(255) NOT NULL,
numero  NVARCHAR(255) NOT NULL,
mes_ex	NVARCHAR(255) NOT NULL,
año_ex  NVARCHAR(255) NOT NULL,
);
GO
ALTER TABLE TC_TARJETA
ADD cv INT CHECK (cv >= 0 AND cv <= 999);

insert into TC_TARJETA (nombre, numero, mes_ex, año_ex, cv) values('Juan Pirir', '1111222233334444','01', '2028',112);
insert into TC_TARJETA (nombre, numero, mes_ex, año_ex, cv) values('Juan Pirir', '2222111133334444','01', '2028',029);
insert into TC_TARJETA (nombre, numero, mes_ex, año_ex, cv) values('Juan Pirir', '3333111122224444','01', '2028',030);
insert into TC_TARJETA (nombre, numero, mes_ex, año_ex, cv) values('Juan Pirir', '4444111122223333','01', '2028',240);
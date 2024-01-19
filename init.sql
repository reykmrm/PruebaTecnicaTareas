-- Conectarse al servidor SQL Server utilizando el usuario sa y la contraseña
-- Cambia 'TuContraseña' por la contraseña de tu elección
-- Esto crea una conexión como el usuario sa para ejecutar el script
-- Esto se hace fuera de cualquier base de datos específica
:CONNECT localhost,1434 -U sa -P Prueba123

-- Crear la base de datos
CREATE DATABASE MiBaseDeDatos;
GO

-- Cambiar al contexto de la base de datos recién creada
USE MiBaseDeDatos;
GO

CREATE TABLE [dbo].[Tareas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Título] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[FechaCreacion] [date] NOT NULL,
	[FechaVancimiento] [date] NOT NULL,
	[Estado] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
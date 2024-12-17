-- 1. Crear la base de datos
CREATE DATABASE InventoryDb;
GO

-- 2. Usar la base de datos creada
USE InventoryDb;
GO

-- 3. Crear la tabla de Usuarios
CREATE TABLE Users (
                       Id INT IDENTITY(1,1) PRIMARY KEY,
                       Username NVARCHAR(50) NOT NULL UNIQUE,
                       PasswordHash NVARCHAR(255) NOT NULL,
                       Role NVARCHAR(50) NOT NULL DEFAULT 'User',
                       CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 4. Crear la tabla de Productos
CREATE TABLE Products (
                          Id INT IDENTITY(1,1) PRIMARY KEY,
                          Name NVARCHAR(100) NOT NULL,
                          Price DECIMAL(10,2) NOT NULL,
                          Description NVARCHAR(500) NULL,
                          Stock INT NOT NULL,
                          Status NVARCHAR(50) NOT NULL,
                          ImageUrl NVARCHAR(255) NULL,
                          IsActive BIT NOT NULL DEFAULT 1,
                          CreatedAt DATETIME DEFAULT GETDATE(),
                          UpdatedAt DATETIME NULL
);
GO

-- 5. Insertar un usuario administrador por defecto
-- Contraseña: 'P@ssw0rd123!' (encriptada con BCrypt)
INSERT INTO Users (Username, PasswordHash, Role)
VALUES ('admin', '$2a$11$QCfutXwYGXHZaZk.JvHZWu8c1BEyXl85SmatcQIOgI7idEFRFCA3u', 'Admin');
GO

-- 6. Insertar productos de ejemplo
INSERT INTO Products (Name, Price, Description, Stock, Status, ImageUrl, IsActive)
VALUES 
('Monitor Gamer MSI 27"', 345.00, 'Monitor Gamer de 27" con resolución FHD 100Hz 1ms', 20, 'Activo', 'https://m.media-amazon.com/images/I/71dy+uMhPmL._AC_SY300_SX300_.jpg', 1),
('Laptop Dell XPS 13', 1200.00, 'Laptop ultrabook Dell XPS de 13 pulgadas', 10, 'Activo', 'https://m.media-amazon.com/images/I/91MXLpouhoL._AC_SL1500_.jpg', 1),
('Teclado Mecánico Redragon', 85.00, 'Teclado mecánico para gamers con switches rojos', 15, 'Activo', 'https://www.computron.com.ec/wp-content/uploads/2023/06/K550-1-SP-1-600x600.jpg', 1),
('Mouse Logitech G502', 60.00, 'Mouse gaming con 16000 DPI y RGB', 30, 'Activo', 'https://nomadaware.com.ec/wp-content/uploads/2020/09/NomadaWare_mouse_logitech_g502.1.jpg', 1);
GO
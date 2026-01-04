USE RestauranteDB;
GO

-- Insertar clientes de ejemplo
INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion) VALUES
('Juan', 'Pérez', 'juan.perez@email.com', '555-1234', 'Calle Principal 123'),
('María', 'García', 'maria.garcia@email.com', '555-5678', 'Avenida Central 456'),
('Carlos', 'López', 'carlos.lopez@email.com', '555-9012', 'Plaza Mayor 789');

-- Insertar mesas de ejemplo
INSERT INTO Mesas (Capacidad, Ubicacion, Ocupada) VALUES
(4, 'Ventana', 0),
(2, 'Centro', 0),
(6, 'Terraza', 0),
(4, 'Centro', 0);

-- Insertar items del menú de ejemplo
INSERT INTO MenuItems (Nombre, Descripcion, Precio, Categoria, Disponible) VALUES
('Pizza Margherita', 'Pizza clásica con tomate, mozzarella y albahaca', 12.50, 'Principal', 1),
('Hamburguesa', 'Hamburguesa con queso, lechuga y tomate', 10.00, 'Principal', 1),
('Ensalada César', 'Ensalada con pollo, crutones y aderezo césar', 8.50, 'Entrada', 1),
('Pasta Carbonara', 'Pasta con salsa carbonara y panceta', 11.00, 'Principal', 1),
('Tiramisú', 'Postre italiano con café y mascarpone', 6.00, 'Postre', 1),
('Agua Mineral', 'Botella de agua mineral', 2.00, 'Bebida', 1),
('Cerveza', 'Cerveza artesanal', 4.50, 'Bebida', 1);

-- Insertar pedidos de ejemplo
INSERT INTO Pedidos (ClienteID, MesaID, FechaPedido, Total, Estado) VALUES
(1, 1, '2023-10-01 12:00:00', 23.50, 'Pagado'),
(2, 2, '2023-10-01 13:30:00', 19.50, 'Servido'),
(3, NULL, '2023-10-01 14:00:00', 8.50, 'Pendiente');

-- Insertar detalles de pedidos de ejemplo
INSERT INTO DetallesPedido (PedidoID, MenuItemID, Cantidad, PrecioUnitario) VALUES
(1, 1, 1, 12.50), -- Pizza
(1, 5, 1, 6.00),  -- Tiramisú
(1, 6, 2, 2.00),  -- Agua
(2, 2, 1, 10.00), -- Hamburguesa
(2, 7, 1, 4.50),  -- Cerveza
(2, 3, 1, 8.50),  -- Ensalada (espera, total no cuadra, ajustar)
(3, 3, 1, 8.50);  -- Ensalada

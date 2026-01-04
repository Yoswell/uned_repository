using System;
using System.Collections.Generic;
using ProyectoRestaurante.DataAccess;
using ProyectoRestaurante.Models;
using ProyectoRestaurante.Services;

namespace ProyectoRestaurante {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== Sistema de Gestión de Restaurante ===");

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Menú Principal:");
                Console.WriteLine("1. Gestionar Clientes");
                Console.WriteLine("2. Gestionar Ítems del Menú");
                Console.WriteLine("3. Gestionar Mesas");
                Console.WriteLine("4. Gestionar Pedidos");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        ManageCustomers();
                        break;
                    case "2":
                        ManageMenuItems();
                        break;
                    case "3":
                        ManageTables();
                        break;
                    case "4":
                        ManageOrders();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void ManageCustomers() {
            CustomerRepository repo = new CustomerRepository();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Clientes:");
                Console.WriteLine("1. Ver todos los clientes");
                Console.WriteLine("2. Agregar cliente");
                Console.WriteLine("3. Actualizar cliente");
                Console.WriteLine("4. Eliminar cliente");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        var customers = repo.GetAllCustomers();
                        foreach (var customer in customers) {
                            Console.WriteLine($"{customer.CustomerID}: {customer.FullName} - {customer.Email}");
                        }
                        break;
                    case "2":
                        AddCustomer(repo);
                        break;
                    case "3":
                        UpdateCustomer(repo);
                        break;
                    case "4":
                        DeleteCustomer(repo);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AddCustomer(CustomerRepository repo) {
            Console.Write("Nombre: ");
            string firstName = Console.ReadLine();
            Console.Write("Apellido: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Teléfono: ");
            string phone = Console.ReadLine();
            Console.Write("Dirección: ");
            string address = Console.ReadLine();

            Customer customer = new Customer {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Address = address
            };

            try {
                repo.AddCustomer(customer);
                Console.WriteLine("Cliente agregado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateCustomer(CustomerRepository repo) {
            Console.Write("ID del cliente: ");
            int id = int.Parse(Console.ReadLine());
            Customer customer = repo.GetCustomerById(id);
            if (customer == null) {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            Console.Write($"Nombre ({customer.FirstName}): ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrEmpty(firstName)) customer.FirstName = firstName;

            Console.Write($"Apellido ({customer.LastName}): ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(lastName)) customer.LastName = lastName;

            Console.Write($"Email ({customer.Email}): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email)) customer.Email = email;

            Console.Write($"Teléfono ({customer.Phone}): ");
            string phone = Console.ReadLine();
            if (!string.IsNullOrEmpty(phone)) customer.Phone = phone;

            Console.Write($"Dirección ({customer.Address}): ");
            string address = Console.ReadLine();
            if (!string.IsNullOrEmpty(address)) customer.Address = address;

            try {
                repo.UpdateCustomer(customer);
                Console.WriteLine("Cliente actualizado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteCustomer(CustomerRepository repo) {
            Console.Write("ID del cliente: ");
            int id = int.Parse(Console.ReadLine());
            try {
                repo.DeleteCustomer(id);
                Console.WriteLine("Cliente eliminado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ManageMenuItems() {
            MenuItemRepository repo = new MenuItemRepository();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Ítems del Menú:");
                Console.WriteLine("1. Ver todos los ítems");
                Console.WriteLine("2. Agregar ítem");
                Console.WriteLine("3. Actualizar ítem");
                Console.WriteLine("4. Eliminar ítem");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        var menuItems = repo.GetAllMenuItems();
                        foreach (var item in menuItems) {
                            Console.WriteLine($"{item.MenuItemID}: {item.Name} - {item.Price:C} ({item.Category}) - {(item.IsAvailable ? "Disponible" : "No disponible")}");
                        }
                        break;
                    case "2":
                        AddMenuItem(repo);
                        break;
                    case "3":
                        UpdateMenuItem(repo);
                        break;
                    case "4":
                        DeleteMenuItem(repo);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AddMenuItem(MenuItemRepository repo) {
            Console.Write("Nombre: ");
            string name = Console.ReadLine();
            Console.Write("Descripción: ");
            string description = Console.ReadLine();
            Console.Write("Precio: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Categoría: ");
            string category = Console.ReadLine();
            Console.Write("Disponible (s/n): ");
            bool available = Console.ReadLine().ToLower() == "s";

            MenuItem item = new MenuItem {
                Name = name,
                Description = description,
                Price = price,
                Category = category,
                IsAvailable = available
            };

            try {
                repo.AddMenuItem(item);
                Console.WriteLine("Ítem agregado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateMenuItem(MenuItemRepository repo) {
            Console.Write("ID del ítem: ");
            int id = int.Parse(Console.ReadLine());
            MenuItem item = repo.GetMenuItemById(id);
            if (item == null) {
                Console.WriteLine("Ítem no encontrado.");
                return;
            }

            Console.Write($"Nombre ({item.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) item.Name = name;

            Console.Write($"Descripción ({item.Description}): ");
            string description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description)) item.Description = description;

            Console.Write($"Precio ({item.Price}): ");
            string priceStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(priceStr)) item.Price = decimal.Parse(priceStr);

            Console.Write($"Categoría ({item.Category}): ");
            string category = Console.ReadLine();
            if (!string.IsNullOrEmpty(category)) item.Category = category;

            Console.Write($"Disponible ({(item.IsAvailable ? "s" : "n")}): ");
            string availableStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(availableStr)) item.IsAvailable = availableStr.ToLower() == "s";

            try {
                repo.UpdateMenuItem(item);
                Console.WriteLine("Ítem actualizado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteMenuItem(MenuItemRepository repo) {
            Console.Write("ID del ítem: ");
            int id = int.Parse(Console.ReadLine());
            try {
                repo.DeleteMenuItem(id);
                Console.WriteLine("Ítem eliminado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ManageTables() {
            TableRepository repo = new TableRepository();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Mesas:");
                Console.WriteLine("1. Ver todas las mesas");
                Console.WriteLine("2. Agregar mesa");
                Console.WriteLine("3. Actualizar mesa");
                Console.WriteLine("4. Eliminar mesa");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        var tables = repo.GetAllTables();
                        foreach (var table in tables) {
                            Console.WriteLine($"{table.TableID}: Capacidad {table.Capacity} - {table.Location} - {(table.IsOccupied ? "Ocupada" : "Libre")}");
                        }
                        break;
                    case "2":
                        AddTable(repo);
                        break;
                    case "3":
                        UpdateTable(repo);
                        break;
                    case "4":
                        DeleteTable(repo);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AddTable(TableRepository repo) {
            Console.Write("Capacidad: ");
            int capacity = int.Parse(Console.ReadLine());
            Console.Write("Ubicación: ");
            string location = Console.ReadLine();
            Console.Write("Ocupada (s/n): ");
            bool occupied = Console.ReadLine().ToLower() == "s";

            Table table = new Table {
                Capacity = capacity,
                Location = location,
                IsOccupied = occupied
            };

            try {
                repo.AddTable(table);
                Console.WriteLine("Mesa agregada exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateTable(TableRepository repo) {
            Console.Write("ID de la mesa: ");
            int id = int.Parse(Console.ReadLine());
            Table table = repo.GetTableById(id);
            if (table == null) {
                Console.WriteLine("Mesa no encontrada.");
                return;
            }

            Console.Write($"Capacidad ({table.Capacity}): ");
            string capacityStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(capacityStr)) table.Capacity = int.Parse(capacityStr);

            Console.Write($"Ubicación ({table.Location}): ");
            string location = Console.ReadLine();
            if (!string.IsNullOrEmpty(location)) table.Location = location;

            Console.Write($"Ocupada ({(table.IsOccupied ? "s" : "n")}): ");
            string occupiedStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(occupiedStr)) table.IsOccupied = occupiedStr.ToLower() == "s";

            try {
                repo.UpdateTable(table);
                Console.WriteLine("Mesa actualizada exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteTable(TableRepository repo) {
            Console.Write("ID de la mesa: ");
            int id = int.Parse(Console.ReadLine());
            try {
                repo.DeleteTable(id);
                Console.WriteLine("Mesa eliminada exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ManageOrders() {
            OrderService service = new OrderService();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Pedidos:");
                Console.WriteLine("1. Crear pedido");
                Console.WriteLine("2. Ver detalles de pedido");
                Console.WriteLine("3. Actualizar estado de pedido");
                Console.WriteLine("4. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        CreateOrder(service);
                        break;
                    case "2":
                        Console.Write("ID del pedido: ");
                        int orderId = int.Parse(Console.ReadLine());
                        service.DisplayOrderDetails(orderId);
                        break;
                    case "3":
                        Console.Write("ID del pedido: ");
                        int oid = int.Parse(Console.ReadLine());
                        Console.Write("Nuevo estado: ");
                        string status = Console.ReadLine();
                        service.UpdateOrderStatus(oid, status);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void CreateOrder(OrderService service) {
            Console.Write("ID del cliente: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("ID de la mesa (opcional): ");
            string tableIdStr = Console.ReadLine();
            int? tableId = string.IsNullOrEmpty(tableIdStr) ? null : int.Parse(tableIdStr);

            List<OrderItem> items = new List<OrderItem>();
            while (true) {
                Console.Write("ID del ítem del menú: ");
                int menuItemId = int.Parse(Console.ReadLine());
                Console.Write("Cantidad: ");
                int quantity = int.Parse(Console.ReadLine());
                items.Add(new OrderItem { MenuItemID = menuItemId, Quantity = quantity });

                Console.Write("¿Agregar otro ítem? (s/n): ");
                if (Console.ReadLine().ToLower() != "s") break;
            }

            service.CreateOrder(customerId, tableId, items);
        }
    }
}

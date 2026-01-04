using System;
using System.Collections.Generic;
using ProyectoRestaurante.DataAccess;
using ProyectoRestaurante.Models;

namespace ProyectoRestaurante.Services {
    public class OrderService {
        private CustomerRepository customerRepo = new CustomerRepository();
        private MenuItemRepository menuItemRepo = new MenuItemRepository();
        private TableRepository tableRepo = new TableRepository();
        private OrderRepository orderRepo = new OrderRepository();
        private OrderItemRepository orderItemRepo = new OrderItemRepository();

        public bool CreateOrder(int customerID, int? tableID, List<OrderItem> items) {
            try {
                // Verificar que el cliente existe
                Customer customer = customerRepo.GetCustomerById(customerID);
                if (customer == null) {
                    Console.WriteLine("Cliente no encontrado.");
                    return false;
                }

                // Verificar mesa si se especifica
                if (tableID.HasValue) {
                    Table table = tableRepo.GetTableById(tableID.Value);
                    if (table == null) {
                        Console.WriteLine("Mesa no encontrada.");
                        return false;
                    }
                    
                    if (table.IsOccupied) {
                        Console.WriteLine("La mesa está ocupada.");
                        return false;
                    }
                }

                // Calcular total
                decimal total = 0;
                foreach (var item in items) {
                    MenuItem menuItem = menuItemRepo.GetMenuItemById(item.MenuItemID);
                    if (menuItem == null || !menuItem.IsAvailable) {
                        Console.WriteLine($"Ítem del menú no disponible: {item.MenuItemID}");
                        return false;
                    }
                    item.UnitPrice = menuItem.Price;
                    total += item.Subtotal;
                }

                // Crear pedido
                Order newOrder = new Order {
                    CustomerID = customerID,
                    TableID = tableID,
                    OrderDate = DateTime.Now,
                    TotalAmount = total,
                    Status = "Pendiente"
                };
                orderRepo.AddOrder(newOrder);

                // Obtener el ID del pedido recién creado (asumiendo auto-increment)
                // Para simplicidad, asumimos que podemos obtener el último ID, pero en realidad necesitaríamos una forma mejor
                // Aquí simplifico, en producción usaríamos SCOPE_IDENTITY o similar
                var orders = orderRepo.GetAllOrders();
                int orderId = orders[orders.Count - 1].OrderID;

                // Agregar items
                foreach (var item in items) {
                    item.OrderID = orderId;
                    orderItemRepo.AddOrderItem(item);
                }

                // Marcar mesa como ocupada si se asignó
                if (tableID.HasValue) {
                    Table table = tableRepo.GetTableById(tableID.Value);
                    table.IsOccupied = true;
                    tableRepo.UpdateTable(table);
                }

                Console.WriteLine("Pedido creado exitosamente.");
                return true;
            } catch (Exception ex) {
                Console.WriteLine($"Error al crear pedido: {ex.Message}");
                return false;
            }
        }

        public void DisplayOrderDetails(int orderID) {
            Order order = orderRepo.GetOrderById(orderID);
            if (order == null) {
                Console.WriteLine("Pedido no encontrado.");
                return;
            }

            Customer customer = customerRepo.GetCustomerById(order.CustomerID);
            Console.WriteLine($"Pedido #{order.OrderID} - Cliente: {customer.FullName} - Total: {order.TotalAmount} - Estado: {order.Status}");

            List<OrderItem> items = orderItemRepo.GetOrderItemsByOrderId(orderID);
            foreach (var item in items) {
                MenuItem menuItem = menuItemRepo.GetMenuItemById(item.MenuItemID);
                Console.WriteLine($"- {menuItem.Name} x{item.Quantity} = {item.Subtotal}");
            }
        }

        public void UpdateOrderStatus(int orderID, string status) {
            try {
                Order order = orderRepo.GetOrderById(orderID);
                if (order == null) {
                    Console.WriteLine("Pedido no encontrado.");
                    return;
                }
                order.Status = status;
                orderRepo.UpdateOrder(order);
                Console.WriteLine("Estado del pedido actualizado.");

                // Si se marca como pagado, liberar mesa
                if (status == "Pagado" && order.TableID.HasValue) {
                    Table table = tableRepo.GetTableById(order.TableID.Value);
                    table.IsOccupied = false;
                    tableRepo.UpdateTable(table);
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error al actualizar estado: {ex.Message}");
            }
        }
    }
}

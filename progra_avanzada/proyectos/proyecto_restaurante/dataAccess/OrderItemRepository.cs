using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoRestaurante.Models;

namespace ProyectoRestaurante.DataAccess {
    public class OrderItemRepository {
        public List<OrderItem> GetOrderItemsByOrderId(int orderId) {
            List<OrderItem> orderItems = new List<OrderItem>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM DetallesPedido WHERE PedidoID = @orderId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    orderItems.Add(new OrderItem {
                        OrderItemID = (int)reader["DetalleID"],
                        OrderID = (int)reader["PedidoID"],
                        MenuItemID = (int)reader["MenuItemID"],
                        Quantity = (int)reader["Cantidad"],
                        UnitPrice = (decimal)reader["PrecioUnitario"]
                    });
                }
            }
            return orderItems;
        }

        public void AddOrderItem(OrderItem orderItem) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO DetallesPedido (PedidoID, MenuItemID, Cantidad, PrecioUnitario)
                                VALUES (@pedidoID, @menuItemID, @cantidad, @precioUnitario)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pedidoID", orderItem.OrderID);
                cmd.Parameters.AddWithValue("@menuItemID", orderItem.MenuItemID);
                cmd.Parameters.AddWithValue("@cantidad", orderItem.Quantity);
                cmd.Parameters.AddWithValue("@precioUnitario", orderItem.UnitPrice);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrderItem(OrderItem orderItem) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"UPDATE DetallesPedido SET PedidoID = @pedidoID, MenuItemID = @menuItemID,
                                Cantidad = @cantidad, PrecioUnitario = @precioUnitario
                                WHERE DetalleID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", orderItem.OrderItemID);
                cmd.Parameters.AddWithValue("@pedidoID", orderItem.OrderID);
                cmd.Parameters.AddWithValue("@menuItemID", orderItem.MenuItemID);
                cmd.Parameters.AddWithValue("@cantidad", orderItem.Quantity);
                cmd.Parameters.AddWithValue("@precioUnitario", orderItem.UnitPrice);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrderItem(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM DetallesPedido WHERE DetalleID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

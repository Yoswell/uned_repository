using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoRestaurante.Models;

namespace ProyectoRestaurante.DataAccess {
    public class OrderRepository {
        public List<Order> GetAllOrders() {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Pedidos";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    orders.Add(new Order {
                        OrderID = (int)reader["PedidoID"],
                        CustomerID = (int)reader["ClienteID"],
                        TableID = reader["MesaID"] as int?,
                        OrderDate = (DateTime)reader["FechaPedido"],
                        TotalAmount = (decimal)reader["Total"],
                        Status = reader["Estado"].ToString()
                    });
                }
            }
            return orders;
        }

        public Order GetOrderById(int id) {
            Order order = null;
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Pedidos WHERE PedidoID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    order = new Order {
                        OrderID = (int)reader["PedidoID"],
                        CustomerID = (int)reader["ClienteID"],
                        TableID = reader["MesaID"] as int?,
                        OrderDate = (DateTime)reader["FechaPedido"],
                        TotalAmount = (decimal)reader["Total"],
                        Status = reader["Estado"].ToString()
                    };
                }
            }
            return order;
        }

        public void AddOrder(Order order) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO Pedidos (ClienteID, MesaID, FechaPedido, Total, Estado)
                                VALUES (@clienteID, @mesaID, @fechaPedido, @total, @estado)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@clienteID", order.CustomerID);
                cmd.Parameters.AddWithValue("@mesaID", order.TableID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaPedido", order.OrderDate);
                cmd.Parameters.AddWithValue("@total", order.TotalAmount);
                cmd.Parameters.AddWithValue("@estado", order.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"UPDATE Pedidos SET ClienteID = @clienteID, MesaID = @mesaID,
                                FechaPedido = @fechaPedido, Total = @total, Estado = @estado
                                WHERE PedidoID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", order.OrderID);
                cmd.Parameters.AddWithValue("@clienteID", order.CustomerID);
                cmd.Parameters.AddWithValue("@mesaID", order.TableID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaPedido", order.OrderDate);
                cmd.Parameters.AddWithValue("@total", order.TotalAmount);
                cmd.Parameters.AddWithValue("@estado", order.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM Pedidos WHERE PedidoID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

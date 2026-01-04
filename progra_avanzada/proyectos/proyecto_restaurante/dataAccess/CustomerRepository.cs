using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoRestaurante.Models;

namespace ProyectoRestaurante.DataAccess {
    public class CustomerRepository {
        public List<Customer> GetAllCustomers() {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Clientes";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    customers.Add(new Customer {
                        CustomerID = (int)reader["ClienteID"],
                        FirstName = reader["Nombre"].ToString(),
                        LastName = reader["Apellido"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Telefono"].ToString(),
                        Address = reader["Direccion"].ToString(),
                        RegistrationDate = (DateTime)reader["FechaRegistro"]
                    });
                }
            }
            return customers;
        }

        public Customer GetCustomerById(int id) {
            Customer customer = null;
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Clientes WHERE ClienteID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    customer = new Customer {
                        CustomerID = (int)reader["ClienteID"],
                        FirstName = reader["Nombre"].ToString(),
                        LastName = reader["Apellido"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Telefono"].ToString(),
                        Address = reader["Direccion"].ToString(),
                        RegistrationDate = (DateTime)reader["FechaRegistro"]
                    };
                }
            }
            return customer;
        }

        public void AddCustomer(Customer customer) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion)
                                VALUES (@nombre, @apellido, @email, @telefono, @direccion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", customer.FirstName);
                cmd.Parameters.AddWithValue("@apellido", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@telefono", customer.Phone);
                cmd.Parameters.AddWithValue("@direccion", customer.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customer) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"UPDATE Clientes SET Nombre = @nombre, Apellido = @apellido,
                                Email = @email, Telefono = @telefono,
                                Direccion = @direccion WHERE ClienteID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer.CustomerID);
                cmd.Parameters.AddWithValue("@nombre", customer.FirstName);
                cmd.Parameters.AddWithValue("@apellido", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@telefono", customer.Phone);
                cmd.Parameters.AddWithValue("@direccion", customer.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM Clientes WHERE ClienteID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

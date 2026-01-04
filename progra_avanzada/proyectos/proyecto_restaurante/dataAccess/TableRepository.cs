using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoRestaurante.Models;

namespace ProyectoRestaurante.DataAccess {
    public class TableRepository {
        public List<Table> GetAllTables() {
            List<Table> tables = new List<Table>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Mesas";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    tables.Add(new Table {
                        TableID = (int)reader["MesaID"],
                        Capacity = (int)reader["Capacidad"],
                        Location = reader["Ubicacion"].ToString(),
                        IsOccupied = (bool)reader["Ocupada"]
                    });
                }
            }
            return tables;
        }

        public Table GetTableById(int id) {
            Table table = null;
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Mesas WHERE MesaID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    table = new Table {
                        TableID = (int)reader["MesaID"],
                        Capacity = (int)reader["Capacidad"],
                        Location = reader["Ubicacion"].ToString(),
                        IsOccupied = (bool)reader["Ocupada"]
                    };
                }
            }
            return table;
        }

        public void AddTable(Table table) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO Mesas (Capacidad, Ubicacion, Ocupada)
                                VALUES (@capacidad, @ubicacion, @ocupada)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@capacidad", table.Capacity);
                cmd.Parameters.AddWithValue("@ubicacion", table.Location);
                cmd.Parameters.AddWithValue("@ocupada", table.IsOccupied);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTable(Table table) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"UPDATE Mesas SET Capacidad = @capacidad, Ubicacion = @ubicacion,
                                Ocupada = @ocupada WHERE MesaID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", table.TableID);
                cmd.Parameters.AddWithValue("@capacidad", table.Capacity);
                cmd.Parameters.AddWithValue("@ubicacion", table.Location);
                cmd.Parameters.AddWithValue("@ocupada", table.IsOccupied);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTable(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM Mesas WHERE MesaID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

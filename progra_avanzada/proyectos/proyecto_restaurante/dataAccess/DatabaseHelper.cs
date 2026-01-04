using System;
using Microsoft.Data.SqlClient;

namespace ProyectoRestaurante.DataAccess {
    public static class DatabaseHelper {
        private static string connectionString = "Server=localhost;Database=RestauranteDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection() {
            return new SqlConnection(connectionString);
        }

        public static void SetConnectionString(string connString) => connectionString = connString;
    }
}

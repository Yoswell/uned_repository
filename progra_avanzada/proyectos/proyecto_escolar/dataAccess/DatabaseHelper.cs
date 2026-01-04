using System;
using Microsoft.Data.SqlClient;

namespace ProyectoEscolar.dataAccess {
    public static class DatabaseHelper {
        private static string connectionString = "Server=localhost;Database=EscuelaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection() {
            return new SqlConnection(connectionString);
        }

        public static void SetConnectionString(string connString) => connectionString = connString;
    }
}

/*== SqlCommand: Crear y ejecutar comandos SQL ==*/
using System;
using Microsoft.Data.SqlClient;

namespace Databases {
    class SqlCommandExample {
        static void Main(string[] args) {
            string connectionString = "Server=VISHOK;Database=TiendaPCGamers;TrustServerCertificate=True;Integrated Security=True;";

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    Console.WriteLine("Conexión abierta.");

                    // Crear comando con consulta INSERT
                    string insertQuery = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID, MarcaID, Imagen, FechaCreacion) " +
                                       "VALUES ('ASUS ROG Strix RTX 5090', 'GPU NVIDIA de gama alta.', 2799.99, 8, 1, 1, 'imagenes/rtx5090.jpg', '2025-11-01 12:00:59')";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection)) {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Producto insertado. Filas afectadas: {rowsAffected}");
                    }

                    // Comando con parámetros para evitar SQL injection
                    string parameterizedQuery = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES (@nombre, @descripcion, @precio, @stock)";

                    using (SqlCommand paramCommand = new SqlCommand(parameterizedQuery, connection)) {
                        // Agregar parámetros
                        paramCommand.Parameters.AddWithValue("@nombre", "Logitech MX Master 3");
                        paramCommand.Parameters.AddWithValue("@descripcion", "Mouse inalámbrico ergonómico");
                        paramCommand.Parameters.AddWithValue("@precio", 99.99m);
                        paramCommand.Parameters.AddWithValue("@stock", 15);

                        int rowsAffected = paramCommand.ExecuteNonQuery();
                        Console.WriteLine($"Producto con parámetros insertado. Filas afectadas: {rowsAffected}");
                    }

                    // Comando para SELECT con ExecuteScalar
                    string countQuery = "SELECT COUNT(*) FROM Productos WHERE Precio > @minPrice";

                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection)) {
                        countCommand.Parameters.AddWithValue("@minPrice", 100.00m);
                        object result = countCommand.ExecuteScalar();
                        int productCount = Convert.ToInt32(result);
                        Console.WriteLine($"Productos con precio > $100: {productCount}");
                    }

                    // Comando con timeout personalizado
                    using (SqlCommand timeoutCommand = new SqlCommand("SELECT TOP 1 * FROM Productos", connection)) {
                        timeoutCommand.CommandTimeout = 30; // 30 segundos
                        using (SqlDataReader reader = timeoutCommand.ExecuteReader()) {
                            if (reader.Read()) {
                                Console.WriteLine($"Primer producto: {reader["Nombre"]}");
                            }
                        }
                    }

                    // Comando con transacción
                    using (SqlTransaction transaction = connection.BeginTransaction()) {
                        try {
                            string updateQuery = "UPDATE Productos SET Stock = Stock + 10 WHERE ProductoID = 1";

                            using (SqlCommand transCommand = new SqlCommand(updateQuery, connection, transaction)) {
                                int rowsAffected = transCommand.ExecuteNonQuery();
                                Console.WriteLine($"Stock actualizado en transacción. Filas: {rowsAffected}");
                            }

                            // Simular error para rollback
                            // throw new Exception("Error simulado");

                            transaction.Commit();
                            Console.WriteLine("Transacción confirmada.");
                        } catch (Exception ex) {
                            transaction.Rollback();
                            Console.WriteLine($"Transacción revertida: {ex.Message}");
                        }
                    }
                }
            } catch (SqlException ex) {
                Console.WriteLine($"Error de SQL: {ex.Message}");
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
            }
        }
    }
}

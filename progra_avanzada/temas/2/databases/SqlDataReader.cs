/*== SqlDataReader: Leer datos de consultas SELECT ==*/
using System;
using Microsoft.Data.SqlClient;

namespace Databases {
    class SqlDataReaderExample {
        static void Main(string[] args) {
            string connectionString = "Server=VISHOK;Database=TiendaPCGamers;TrustServerCertificate=True;Integrated Security=True;";

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    // Consulta SELECT
                    string selectQuery = "SELECT ProductoID, Nombre, Descripcion, Precio, Stock FROM Productos WHERE Stock > 0";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                // Acceso por índice de columna
                                int id = reader.GetInt32(0);
                                string nombre = reader.GetString(1);
                                string descripcion = reader.IsDBNull(2) ? "Sin descripción" : reader.GetString(2);
                                decimal precio = reader.GetDecimal(3);
                                int stock = reader.GetInt32(4);

                                Console.WriteLine($"{id}\t{nombre.PadRight(20)}\t${precio:N2}\t{stock}");
                            }
                        }
                    }

                    // Ejemplo con acceso por nombre de columna
                    string namedQuery = "SELECT Nombre, Precio FROM Productos WHERE Precio BETWEEN 50 AND 200";

                    using (SqlCommand namedCommand = new SqlCommand(namedQuery, connection)) {
                        using (SqlDataReader reader = namedCommand.ExecuteReader()) {
                            Console.WriteLine("\nProductos en rango de precio $50-$200:");
                            while (reader.Read()) {
                                string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                decimal precio = reader.GetDecimal(reader.GetOrdinal("Precio"));
                                Console.WriteLine($"{nombre}: ${precio:N2}");
                            }
                        }
                    }

                    // Ejemplo con diferentes tipos de datos
                    string typesQuery = "SELECT ProductoID, Nombre, Precio, Stock, FechaCreacion FROM Productos WHERE ProductoID <= 5";

                    using (SqlCommand typesCommand = new SqlCommand(typesQuery, connection)) {
                        using (SqlDataReader reader = typesCommand.ExecuteReader()) {
                            Console.WriteLine("\nPrimeros 5 productos con tipos de datos:");
                            while (reader.Read()) {
                                // Usando GetXxx por nombre
                                int id = reader.GetInt32("ProductoID");
                                string nombre = reader.GetString("Nombre");
                                decimal precio = reader.GetDecimal("Precio");
                                int stock = reader.GetInt32("Stock");
                                DateTime fecha = reader.GetDateTime("FechaCreacion");

                                Console.WriteLine($"ID: {id}, Nombre: {nombre}, Precio: ${precio}, Stock: {stock}, Fecha: {fecha:dd/MM/yyyy}");
                            }
                        }
                    }

                    // Ejemplo con verificación de valores nulos
                    string nullCheckQuery = "SELECT Nombre, Descripcion FROM Productos";

                    using (SqlCommand nullCommand = new SqlCommand(nullCheckQuery, connection)) {
                        using (SqlDataReader reader = nullCommand.ExecuteReader()) {
                            Console.WriteLine("\nVerificación de valores nulos:");
                            while (reader.Read()) {
                                string nombre = reader.GetString("Nombre");
                                string descripcion = reader.IsDBNull("Descripcion") ? "Sin descripción" : reader.GetString("Descripcion");
                                Console.WriteLine($"{nombre}: {descripcion}");
                            }
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

/*== ExecuteScalar: Ejecutar consultas que devuelven un solo valor ==*/
using System;
using Microsoft.Data.SqlClient;

namespace Databases {
    class ExecuteScalarExample {
        static void Main(string[] args) {
            string connectionString = "Server=VISHOK;Database=TiendaPCGamers;TrustServerCertificate=True;Integrated Security=True;";

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    Console.WriteLine("Conexión a la base de datos establecida.");

                    // INSERT con OUTPUT para obtener el ID generado
                    string insertQuery = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID, MarcaID, Imagen, FechaCreacion) " +
                                        "OUTPUT INSERTED.ProductoID " +
                                        "VALUES ('ASUS ROG Strix RTX 5090', 'GPU NVIDIA de gama alta.', 2799.99, 8, 1, 1, 'imagenes/rtx5090.jpg', '2025-11-01 12:00:59')";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection)) {
                        object result = insertCommand.ExecuteScalar();
                        if (result != null) {
                            int newProductId = Convert.ToInt32(result);
                            Console.WriteLine($"Producto insertado con ID: {newProductId}");
                        }
                    }

                    // SELECT COUNT: Contar productos
                    string countQuery = "SELECT COUNT(*) FROM Productos";

                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection)) {
                        object countResult = countCommand.ExecuteScalar();
                        int totalProducts = Convert.ToInt32(countResult);
                        Console.WriteLine($"Total de productos: {totalProducts}");
                    }

                    // SELECT MAX: Precio máximo
                    string maxPriceQuery = "SELECT MAX(Precio) FROM Productos";

                    using (SqlCommand maxPriceCommand = new SqlCommand(maxPriceQuery, connection)) {
                        object maxPriceResult = maxPriceCommand.ExecuteScalar();
                        if (maxPriceResult != DBNull.Value) {
                            decimal maxPrice = Convert.ToDecimal(maxPriceResult);
                            Console.WriteLine($"Precio máximo: ${maxPrice:N2}");
                        }
                    }

                    // SELECT SUM: Suma total del stock
                    string sumStockQuery = "SELECT SUM(Stock) FROM Productos";

                    using (SqlCommand sumStockCommand = new SqlCommand(sumStockQuery, connection)) {
                        object sumStockResult = sumStockCommand.ExecuteScalar();
                        if (sumStockResult != DBNull.Value) {
                            int totalStock = Convert.ToInt32(sumStockResult);
                            Console.WriteLine($"Stock total: {totalStock} unidades");
                        }
                    }

                    // SELECT con funciones de agregado
                    string avgPriceQuery = "SELECT AVG(Precio) FROM Productos WHERE Precio > 100";

                    using (SqlCommand avgPriceCommand = new SqlCommand(avgPriceQuery, connection)) {
                        object avgPriceResult = avgPriceCommand.ExecuteScalar();
                        if (avgPriceResult != DBNull.Value) {
                            decimal avgPrice = Convert.ToDecimal(avgPriceResult);
                            Console.WriteLine($"Precio promedio (productos > $100): ${avgPrice:N2}");
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

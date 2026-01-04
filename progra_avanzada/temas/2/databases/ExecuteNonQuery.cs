/*== ExecuteNonQuery: Ejecutar comandos que no devuelven filas (INSERT, UPDATE, DELETE) ==*/
using System;
using Microsoft.Data.SqlClient;

namespace Databases {
    class ExecuteNonQueryExample {
        static void Main(string[] args) {
            string connectionString = "Server=VISHOK;Database=TiendaPCGamers;TrustServerCertificate=True;Integrated Security=True;";

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    Console.WriteLine("Conexión a la base de datos establecida.");

                    // INSERT: Agregar un nuevo producto
                    string insertQuery = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID, MarcaID, Imagen, FechaCreacion) " +
                                        "VALUES ('ASUS ROG Strix RTX 5090', 'GPU NVIDIA de gama alta.', 2799.99, 8, 1, 1, 'imagenes/rtx5090.jpg', '2025-11-01 12:00:59')";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection)) {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"INSERT: {rowsAffected} fila(s) afectada(s).");
                    }

                    // UPDATE: Actualizar el precio de un producto
                    string updateQuery = "UPDATE Productos SET Precio = Precio * 1.1 WHERE Nombre LIKE '%RTX%'";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection)) {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        Console.WriteLine($"UPDATE: {rowsAffected} fila(s) afectada(s).");
                    }

                    // DELETE: Eliminar productos con stock cero
                    string deleteQuery = "DELETE FROM Productos WHERE Stock = 0";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection)) {
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        Console.WriteLine($"DELETE: {rowsAffected} fila(s) afectada(s).");
                    }

                    // CREATE TABLE: Crear una tabla de logs
                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Logs' AND xtype='U')
                        CREATE TABLE Logs (
                            LogID INT PRIMARY KEY IDENTITY(1,1),
                            Mensaje NVARCHAR(500),
                            Fecha DATETIME DEFAULT GETDATE()
                        )";

                    using (SqlCommand createCommand = new SqlCommand(createTableQuery, connection)) {
                        createCommand.ExecuteNonQuery();
                        Console.WriteLine("Tabla Logs creada o ya existe.");
                    }

                    // INSERT en la tabla de logs
                    string insertLogQuery = "INSERT INTO Logs (Mensaje) VALUES ('Operaciones de ejemplo ejecutadas exitosamente')";

                    using (SqlCommand logCommand = new SqlCommand(insertLogQuery, connection)) {
                        int rowsAffected = logCommand.ExecuteNonQuery();
                        Console.WriteLine($"Log insertado: {rowsAffected} fila(s) afectada(s).");
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

/*== SqlConnection: Establecer conexiones a SQL Server ==*/
using System;
using Microsoft.Data.SqlClient;

namespace Databases {
    class SqlConnectionExample {
        static void Main(string[] args) {
            // Ejemplos de parámetros de cadena de conexión
            /*
                Server=DESKTOP-T5BBINC,1433;           // Servidor y puerto
                Database=PcGamersDB;                   // Base de datos
                Username=miUsuario;                    // Usuario SQL
                Password=miContraseña;                 // Contraseña
                TrustServerCertificate=True;           // Certificado SSL
                Integrated Security=False;             // No usar Windows Auth
                Connection Timeout=30;                 // Timeout en segundos
                Pooling=true;                          // Usar connection pooling
                Min Pool Size=5;                       // Mínimo de conexiones
                Max Pool Size=100;                     // Máximo de conexiones
                MultipleActiveResultSets=true;         // Múltiples resultados
                Encrypt=true;                          // Encriptar conexión
            */

            string connectionString = "Server=VISHOK;Database=TiendaPCGamers;TrustServerCertificate=True;Integrated Security=True;";

            try {
                // Forma tradicional de manejar conexiones (requiere Dispose manual)
                SqlConnection traditionalConnection = new SqlConnection(connectionString);
                traditionalConnection.Open();
                Console.WriteLine("Conexión abierta con método tradicional.");
                traditionalConnection.Close();
                traditionalConnection.Dispose();

                // Forma moderna usando 'using' (automático Dispose)
                using (SqlConnection modernConnection = new SqlConnection(connectionString)) {
                    modernConnection.Open();
                    Console.WriteLine("Conexión abierta con 'using'.");

                    // Verificar estado de la conexión
                    Console.WriteLine($"Estado de la conexión: {modernConnection.State}");

                    // Obtener información del servidor
                    string serverVersion = modernConnection.ServerVersion;
                    Console.WriteLine($"Versión del servidor: {serverVersion}");

                    // Ejecutar una consulta simple
                    string testQuery = "SELECT @@VERSION";
                    using (SqlCommand command = new SqlCommand(testQuery, modernConnection)) {
                        string version = (string)command.ExecuteScalar();
                        Console.WriteLine($"Versión de SQL Server: {version.Substring(0, 50)}...");
                    }
                }

                // Ejemplo con SqlConnectionStringBuilder
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "VISHOK";
                builder.InitialCatalog = "TiendaPCGamers";
                builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                builder.ConnectTimeout = 30;

                string builtConnectionString = builder.ConnectionString;
                Console.WriteLine("Cadena de conexión construida:");
                Console.WriteLine(builtConnectionString);

                // Probar conexión con la cadena construida
                using (SqlConnection builtConnection = new SqlConnection(builtConnectionString)) {
                    builtConnection.Open();
                    Console.WriteLine("Conexión exitosa con cadena construida.");
                }

            } catch (SqlException ex) {
                Console.WriteLine($"Error de SQL: {ex.Message}");
                Console.WriteLine($"Número de error: {ex.Number}");
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
            }
        }
    }
}

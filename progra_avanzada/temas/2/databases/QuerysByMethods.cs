/*== Consultas SQL organizadas por métodos ==*/
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Databases {
    class QueriesByMethodsExample {
        // Cadena de conexión a la base de datos SQL Server
        private string _connectionString = "Server=VISHOK;Database=TiendaPCGamers;TrustServerCertificate=True;Integrated Security=True;";

        static void Main(string[] args) {
            QueriesByMethodsExample program = new QueriesByMethodsExample();
            bool exit = false;

            while (!exit) {
                Console.WriteLine("1. Agregar cliente");
                Console.WriteLine("2. Listar clientes");
                Console.WriteLine("3. Buscar cliente ");
                Console.WriteLine("0. Salir          ");
                Console.Write();
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int option)) {
                    switch (option) {
                        case 1:
                            program.AddClient();
                            break;
                        case 2:
                            program.ListClients();
                            break;
                        case 3:
                            program.SearchClient();
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                } else {
                    Console.WriteLine("Entrada no válida. Intente de nuevo.");
                }

                Console.WriteLine();
                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void AddClient() {
            Console.Write("\nNombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();
            DateTime fechaRegistro = DateTime.Now;

            try {
                InsertClient(nombre, apellido, email, telefono, fechaRegistro);
                Console.WriteLine("Cliente agregado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error al agregar cliente: {ex.Message}");
            }
        }

        private void ListClients() {
            try {
                GetClients();
            } catch (Exception ex) {
                Console.WriteLine($"Error al listar clientes: {ex.Message}");
            }
        }

        private void SearchClient() {
            Console.Write("Nombre a buscar: ");
            string nombreSearch = Console.ReadLine();

            try {
                SearchClientByName(nombreSearch);
            } catch (Exception ex) {
                Console.WriteLine($"Error al buscar cliente: {ex.Message}");
            }
        }

        private void InsertClient(string nombre, string apellido, string email, string telefono, DateTime fechaRegistro) {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();

                string insertQuery = "INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, FechaRegistro)" +
                                    "VALUES (@Nombre, @Apellido, @Email, @Telefono, @FechaRegistro)";

                using (SqlCommand command = new SqlCommand(insertQuery, conn)) {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void GetClients() {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();

                string selectQuery = "SELECT ClienteID, Nombre, Apellido, Email, Telefono, FechaRegistro FROM Clientes ORDER BY Nombre";

                using (SqlCommand command = new SqlCommand(selectQuery, conn)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            int id = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            string apellido = reader.GetString(2);
                            string email = reader.GetString(3);
                            string telefono = reader.IsDBNull(4) ? "N/A" : reader.GetString(4);
                            DateTime fecha = reader.GetDateTime(5);

                            Console.WriteLine($"{id}\t{nombre.PadRight(15)}\t{apellido.PadRight(15)}\t{email.PadRight(20)}\t{telefono}\t{fecha:dd/MM/yyyy}");
                        }
                    }
                }
            }
        }

        private void SearchClientByName(string nombre) {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();

                // Usar parámetros para evitar SQL injection
                string searchQuery = "SELECT ClienteID, Nombre, Apellido, Email, Telefono, FechaRegistro" +
                                    "FROM Clientes WHERE Nombre LIKE @Nombre ORDER BY Nombre";

                using (SqlCommand command = new SqlCommand(searchQuery, conn)) {
                    command.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        bool found = false;
                        while (reader.Read()) {
                            found = true;
                            int id = reader.GetInt32(0);
                            string nombreResult = reader.GetString(1);
                            string apellido = reader.GetString(2);
                            string email = reader.GetString(3);
                            string telefono = reader.IsDBNull(4) ? "N/A" : reader.GetString(4);
                            DateTime fecha = reader.GetDateTime(5);

                            Console.WriteLine($"{id}\t{nombreResult.PadRight(15)}\t{apellido.PadRight(15)}\t{email.PadRight(20)}\t{telefono}\t{fecha:dd/MM/yyyy}");
                        }

                        if (!found) {
                            Console.WriteLine("No se encontraron clientes con ese nombre.");
                        }
                    }
                }
            }
        }
    }
}

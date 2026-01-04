/*== Menú de ejemplo por consola implementando colecciones y switches ==*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Menus {
    class MenuProgram {
        private static List<string> users = new List<string>();

        static void Main(string[] args) => ShowMainMenu();

        private static void ShowMainMenu() {
            bool exit = false;

            while (!exit) {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║            MENÚ PRINCIPAL            ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Gestión de Usuarios Básica        ║");
                Console.WriteLine("║ 2. Proyecto Escolar (Estudiantes)    ║");
                Console.WriteLine("║ 3. Proyecto Restaurante (Pedidos)    ║");
                Console.WriteLine("║ 0. Salir                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string input = Console.ReadLine();

                switch (input) {
                    case "1":
                        ShowUserMenu();
                        break;
                    case "2":
                        ShowSchoolMenu();
                        break;
                    case "3":
                        ShowRestaurantMenu();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowUserMenu() {
            bool exit = false;

            while (!exit) {
                Console.Clear();
                Console.WriteLine("+---+--------------------+");
                Console.WriteLine("| 1 | Agregar usuario    |");
                Console.WriteLine("| 2 | Listar usuarios    |");
                Console.WriteLine("| 3 | Buscar usuario     |");
                Console.WriteLine("| 4 | Eliminar usuario   |");
                Console.WriteLine("| 0 | Volver             |");
                Console.WriteLine("+---+--------------------+");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string input = Console.ReadLine();

                switch (input) {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        ListUsers();
                        break;
                    case "3":
                        SearchUser();
                        break;
                    case "4":
                        RemoveUser();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowSchoolMenu() {
            bool exit = false;

            while (!exit) {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║            GESTIÓN ESCOLA            ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Gestionar Estudiantes             ║");
                Console.WriteLine("║ 2. Gestionar Cursos                  ║");
                Console.WriteLine("║ 3. Gestionar Matrículas              ║");
                Console.WriteLine("║ 0. Volver al menú principal          ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string input = Console.ReadLine();

                switch (input) {
                    case "1":
                        ManageStudents();
                        break;
                    case "2":
                        ManageCourses();
                        break;
                    case "3":
                        ManageEnrollments();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowRestaurantMenu() {
            bool exit = false;

            while (!exit) {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║        GESTIÓN DE RESTAURANTE        ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Gestionar Clientes                ║");
                Console.WriteLine("║ 2. Gestionar Ítems del Menú          ║");
                Console.WriteLine("║ 3. Gestionar Mesas                   ║");
                Console.WriteLine("║ 4. Gestionar Pedidos                 ║");
                Console.WriteLine("║ 0. Volver al menú principal          ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string input = Console.ReadLine();

                switch (input) {
                    case "1":
                        ManageCustomers();
                        break;
                    case "2":
                        ManageMenuItems();
                        break;
                    case "3":
                        ManageTables();
                        break;
                    case "4":
                        ManageOrders();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Métodos para usuarios básicos
        private static void AddUser() {
            Console.Write("Ingrese el nombre del usuario: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) {
                users.Add(name);
                Console.WriteLine("Usuario agregado exitosamente.");
            } else {
                Console.WriteLine("Nombre inválido.");
            }
            Console.ReadKey();
        }

        private static void ListUsers() {
            Console.WriteLine("Lista de usuarios:");
            if (users.Count == 0) {
                Console.WriteLine("No hay usuarios registrados.");
            } else {
                foreach (var user in users) Console.WriteLine($"- {user}");
            }
            Console.ReadKey();
        }

        private static void SearchUser() {
            Console.Write("Ingrese la letra inicial: ");
            string letter = Console.ReadLine();
            if (!string.IsNullOrEmpty(letter)) {
                var foundUsers = users.Where(name => name.StartsWith(letter, StringComparison.OrdinalIgnoreCase)).ToList();
                if (foundUsers.Count == 0) {
                    Console.WriteLine("No se encontraron usuarios.");
                } else {
                    Console.WriteLine("Usuarios encontrados:");
                    foreach (var user in foundUsers) {
                        Console.WriteLine($"- {user}");
                    }
                }
            } else {
                Console.WriteLine("Letra inválida.");
            }
            Console.ReadKey();
        }

        private static void RemoveUser() {
            Console.Write("Ingrese el nombre del usuario a eliminar: ");
            string name = Console.ReadLine();
            if (users.Remove(name)) {
                Console.WriteLine("Usuario eliminado exitosamente.");
            } else {
                Console.WriteLine("Usuario no encontrado.");
            }
            Console.ReadKey();
        }

        // Métodos simulados para escuela (sin BD real)
        private static void ManageStudents() {
            Console.WriteLine("Funcionalidad de gestión de estudiantes (simulada).");
            Console.WriteLine("En el proyecto real, aquí se conectarían a la BD.");
            Console.ReadKey();
        }

        private static void ManageCourses() {
            Console.WriteLine("Funcionalidad de gestión de cursos (simulada).");
            Console.ReadKey();
        }

        private static void ManageEnrollments() {
            Console.WriteLine("Funcionalidad de gestión de matrículas (simulada).");
            Console.ReadKey();
        }

        // Métodos simulados para restaurante
        private static void ManageCustomers() {
            Console.WriteLine("Funcionalidad de gestión de clientes (simulada).");
            Console.ReadKey();
        }

        private static void ManageMenuItems() {
            Console.WriteLine("Funcionalidad de gestión de ítems del menú (simulada).");
            Console.ReadKey();
        }

        private static void ManageTables() {
            Console.WriteLine("Funcionalidad de gestión de mesas (simulada).");
            Console.ReadKey();
        }

        private static void ManageOrders() {
            Console.WriteLine("Funcionalidad de gestión de pedidos (simulada).");
            Console.ReadKey();
        }
    }
}

/*== Definición de propiedades get y set para atributos de una clase ==*/
using System;

namespace Particularities {
    // Forma automática (auto-implemented properties)
    class Client {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // Forma custom con lógica adicional
    class Employee {
        private string _name;
        private int _age;

        public string Name {
            get { return _name; }
            set {
                if (string.IsNullOrEmpty(value)) 
                    throw new ArgumentException("El nombre no puede estar vacío");
                _name = value;
            }
        }

        public int Age {
            get { return _age; }
            set {
                if (value < 0 || value > 120) 
                    throw new ArgumentException("La edad debe estar entre 0 y 120");
                _age = value;
            }
        }

        // Propiedad de solo lectura
        public string FullInfo => $"{Name}, {Age} años";

        // Propiedad con backing field
        private decimal _salary;
        public decimal Salary {
            get => _salary;
            set => _salary = value > 0 ? value : 0;
        }
    }

    class Program {
        static void Main(string[] args) {
            // Usar clase con propiedades automáticas
            Client client = new Client {
                Name = "Ana García",
                Age = 30
            };
            Console.WriteLine($"Cliente: {client.Name}, Edad: {client.Age}");

            // Usar clase con propiedades custom
            Employee employee = new Employee();
            employee.Name = "Carlos López";
            employee.Age = 25;
            employee.Salary = 50000;

            Console.WriteLine($"Empleado: {employee.Name}, Edad: {employee.Age}, Salario: ${employee.Salary}");
            Console.WriteLine($"Información completa: {employee.FullInfo}");

            // Intentar valores inválidos
            try {
                employee.Age = -5;
            } catch (ArgumentException ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            try {
                employee.Name = "";
            } catch (ArgumentException ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

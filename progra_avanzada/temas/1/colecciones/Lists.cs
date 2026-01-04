/*== Colecciones, almacenamiento temporal (no persistente) de datos ==*/
using System;
using System.Collections.Generic;

namespace Collections {
    class ListExample {
        static void Main(string[] args) {
            // Crear una lista de frutas
            List<string> fruits = new List<string> { "Manzana", "Banana", "Naranja" };

            // Agregar elementos (uno o varios)
            fruits.Add("Pera");
            fruits.AddRange(new[] { "Uva", "Sandía" });

            Console.WriteLine("Lista de frutas:");
            foreach (string fruit in fruits) Console.WriteLine($"- {fruit}");

            // Buscar elementos
            bool exists = fruits.Contains("Manzana");
            int index = fruits.IndexOf("Banana");
            Console.WriteLine($"¿Existe Manzana? {exists}");
            Console.WriteLine($"Índice de Banana: {index}");

            // Insertar en posición específica
            fruits.Insert(1, "Kiwi");
            Console.WriteLine("Después de insertar Kiwi en posición 1:");
            foreach (string fruit in fruits) Console.WriteLine($"- {fruit}");

            // Remover elemento
            fruits.Remove("Banana");
            Console.WriteLine("Después de remover Banana:");
            foreach (string fruit in fruits) Console.WriteLine($"- {fruit}");

            // Lista de objetos complejos
            List<Person> people = new List<Person> {
                new Person { Name = "Ana", Age = 25 },
                new Person { Name = "Luis", Age = 30 }
            };

            Console.WriteLine("Lista de personas:");
            foreach (Person person in people) Console.WriteLine($"- {person.Name}, {person.Age} años");

            // Agregar más personas
            people.Add(new Person { Name = "María", Age = 28 });
            Console.WriteLine("Después de agregar María:");
            foreach (Person person in people) Console.WriteLine($"- {person.Name}, {person.Age} años");
        }
    }

    class Person {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

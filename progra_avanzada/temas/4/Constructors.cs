/*== Constructores en C# ==*/
using System;

namespace Constructors {
    class Person {
        public string Name { get; set; }
        public int Age { get; set; }
        public static int Count { get; private set; }

        // Constructor por defecto
        public Person() {
            Name = "Desconocido";
            Age = 0;
            Count++;
        }

        // Constructor parametrizado
        public Person(string name, int age) {
            Name = name;
            Age = age;
            Count++;
        }

        // Constructor de copia
        public Person(Person other) {
            Name = other.Name;
            Age = other.Age;
            Count++;
        }

        // Destructor (no común en C#, pero para ejemplo)
        ~Person() {
            Count--;
        }

        public void Display() => Console.WriteLine($"Nombre: {Name}, Edad: {Age}");
    }

    class ConstructorsExample {
        static void Main(string[] args) {
            // Usar constructor por defecto
            Person p1 = new Person();
            p1.Display();

            // Usar constructor parametrizado
            Person p2 = new Person("Ana", 25);
            p2.Display();

            // Usar constructor de copia
            Person p3 = new Person(p2);
            p3.Name = "Luis"; // Cambiar para mostrar que es copia
            p3.Display();

            Console.WriteLine($"Total de personas creadas: {Person.Count}");

            // Forzar recolección de basura para ver destructor
            p1 = null;
            p2 = null;
            p3 = null;
            GC.Collect();
            Console.WriteLine($"Después de GC: {Person.Count}");
        }
    }
}

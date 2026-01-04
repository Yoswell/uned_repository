/*== Reflexión en C# ==*/
using System;
using System.Reflection;

namespace Reflection {
    class Person {
        public string Name { get; set; }
        public int Age { get; set; }

        public void Greet() => Console.WriteLine($"Hola, soy {Name}.");

        private void PrivateMethod() => Console.WriteLine("Método privado llamado.");
    }

    class ReflectionExample {
        static void Main(string[] args) {
            Person person = new Person { Name = "Ana", Age = 25 };

            Type type = person.GetType();
            Console.WriteLine($"Tipo: {type.Name}");

            // Obtener propiedades
            PropertyInfo[] properties = type.GetProperties();
            Console.WriteLine("Propiedades:");
            foreach (PropertyInfo prop in properties) {
                object value = prop.GetValue(person);
                Console.WriteLine($"{prop.Name}: {value}");
            }

            // Obtener métodos
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine("Métodos públicos:");
            foreach (MethodInfo method in methods) Console.WriteLine(method.Name);

            // Invocar método
            MethodInfo greetMethod = type.GetMethod("Greet");
            greetMethod.Invoke(person, null);

            // Invocar método privado
            MethodInfo privateMethod = type.GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            privateMethod.Invoke(person, null);

            // Crear instancia usando reflexión
            Type personType = typeof(Person);
            Person newPerson = (Person)Activator.CreateInstance(personType);
            newPerson.Name = "Luis";
            newPerson.Greet();
        }
    }
}

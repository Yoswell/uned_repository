/*== HashSet, almacenamiento temporal de datos (no duplicados) ==*/
using System;
using System.Collections.Generic;

namespace Collections {
    class HashSetExample {
        static void Main(string[] args) {
            // Crear un HashSet con números (elimina duplicados automáticamente)
            HashSet<int> uniqueNumbers = new HashSet<int> { 1, 2, 3, 3, 4, 4, 5 };

            Console.WriteLine("Números únicos:");
            foreach (int number in uniqueNumbers) Console.WriteLine(number);

            // Agregar elementos
            uniqueNumbers.Add(6);
            uniqueNumbers.Add(2); // No se agrega porque ya existe

            Console.WriteLine("Después de agregar 6 y 2:");
            foreach (int number in uniqueNumbers) Console.WriteLine(number);

            // Verificar si contiene un elemento
            bool contains3 = uniqueNumbers.Contains(3);
            bool contains7 = uniqueNumbers.Contains(7);
            Console.WriteLine($"¿Contiene 3? {contains3}");
            Console.WriteLine($"¿Contiene 7? {contains7}");

            // Remover elemento
            uniqueNumbers.Remove(4);
            Console.WriteLine("Después de remover 4:");
            foreach (int number in uniqueNumbers) Console.WriteLine(number);

            // HashSet con strings
            HashSet<string> uniqueNames = new HashSet<string> { "Ana", "Luis", "Ana", "María" };
            Console.WriteLine("Nombres únicos:");
            foreach (string name in uniqueNames) Console.WriteLine(name);
        }
    }
}

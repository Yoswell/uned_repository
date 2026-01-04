/*== Language Integrated Query ==*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Particularities {
    class LinqExample {
        static void Main(string[] args) {
            List<string> names = new List<string> {
                "Maria",
                "Pedro",
                "Julian",
                "Ana",
                "Amanda"
            };

            // Forma tradicional sin LINQ
            Console.WriteLine("Nombres que empiezan con 'A' (sin LINQ):");
            foreach (string name in names) {
                if (name.StartsWith("A")) Console.WriteLine(name);
            }

            // Forma moderna usando LINQ (sintaxis de consulta)
            var filteredNames1 = from name in names where name.StartsWith("A") select name;

            Console.WriteLine("Nombres que empiezan con 'A' (LINQ consulta):");
            foreach (string name in filteredNames1) Console.WriteLine(name);

            // Forma moderna usando LINQ (métodos de extensión)
            var filteredNames2 = names.Where(name => name.StartsWith("A"));

            Console.WriteLine("Nombres que empiezan con 'A' (LINQ métodos):");
            foreach (string name in filteredNames2) Console.WriteLine(name);

            // Más ejemplos de LINQ
            var orderedNames = names.OrderBy(name => name.Length);
            Console.WriteLine("Nombres ordenados por longitud:");
            foreach (string name in orderedNames) Console.WriteLine($"{name} (longitud: {name.Length})");

            var countStartingWithA = names.Count(name => name.StartsWith("A"));
            Console.WriteLine($"Cantidad de nombres que empiezan con 'A': {countStartingWithA}");

            var firstNameWithA = names.FirstOrDefault(name => name.StartsWith("A"));
            Console.WriteLine($"Primer nombre que empieza con 'A': {firstNameWithA}");
        }
    }
}

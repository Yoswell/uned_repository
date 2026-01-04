/*== Diccionarios, almacenamiento temporal de datos mediante (key, value) ==*/
using System;
using System.Collections.Generic;

namespace Collections {
    class DictionaryExample {
        static void Main(string[] args) {
            // Crear un diccionario de países y capitales
            Dictionary<string, string> capitals = new Dictionary<string, string> {
                { "España", "Madrid" },
                { "Francia", "París" },
                { "Italia", "Roma" }
            };

            // Agregar elementos
            capitals.Add("Alemania", "Berlín");
            capitals["Portugal"] = "Lisboa";

            // Acceder a valores usando la clave
            if (capitals.ContainsKey("España")) {
                string capital = capitals["España"];
                Console.WriteLine($"La capital de España es {capital}");
            }

            // Verificar si existe una clave
            if (capitals.ContainsKey("Reino Unido")) {
                Console.WriteLine("Reino Unido está en el diccionario");
            } else {
                Console.WriteLine("Reino Unido no está en el diccionario");
            }

            // Recorrer el diccionario
            Console.WriteLine("Lista de países y capitales:");
            foreach (KeyValuePair<string, string> kvp in capitals) Console.WriteLine($"País: {kvp.Key}, Capital: {kvp.Value}");

            // Remover un elemento
            capitals.Remove("Italia");
            Console.WriteLine("Después de remover Italia:");
            foreach (var kvp in capitals) Console.WriteLine($"País: {kvp.Key}, Capital: {kvp.Value}");

            // Diccionario con objetos como valor
            Dictionary<int, Product> products = new Dictionary<int, Product> {
                { 1, new Product("Laptop", 999.99m) },
                { 2, new Product("Mouse", 25.50m) }
            };

            Console.WriteLine("Lista de productos:");
            foreach (var kvp in products) Console.WriteLine($"ID: {kvp.Key}, Nombre: {kvp.Value.Name}, Precio: ${kvp.Value.Price}");

            // Agregar más productos
            products[3] = new Product("Teclado", 75.00m);
            Console.WriteLine("Después de agregar teclado:");
            foreach (var kvp in products) Console.WriteLine($"ID: {kvp.Key}, Nombre: {kvp.Value.Name}, Precio: ${kvp.Value.Price}");
        }
    }

    class Product {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price) {
            Name = name;
            Price = price;
        }
    }
}

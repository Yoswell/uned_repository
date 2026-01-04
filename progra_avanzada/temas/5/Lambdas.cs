/*== Expresiones Lambda en C# ==*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambdas {
    class LambdasExample {
        static void Main(string[] args) {
            // Lista de números
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Lambda para filtrar números pares
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            Console.WriteLine("Números pares:");
            foreach (int num in evenNumbers) Console.WriteLine(num);

            // Lambda para transformar números
            var squaredNumbers = numbers.Select(n => n * n);
            Console.WriteLine("Números al cuadrado:");
            foreach (int num in squaredNumbers) Console.WriteLine(num);

            // Lambda con múltiples parámetros
            Func<int, int, int> add = (a, b) => a + b;
            Console.WriteLine($"Suma: {add(5, 3)}");

            // Lambda sin parámetros
            Action greet = () => Console.WriteLine("¡Hola desde lambda!");
            greet();

            // Lambda en delegates
            Predicate<int> isEven = n => n % 2 == 0;
            Console.WriteLine($"¿6 es par? {isEven(6)}");
            Console.WriteLine($"¿7 es par? {isEven(7)}");
        }
    }
}

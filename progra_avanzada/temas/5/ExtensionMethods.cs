/*== Métodos de Extensión en C# ==*/
using System;

namespace ExtensionMethods {
    // Clase estática para métodos de extensión
    public static class StringExtensions {
        // Método de extensión para contar palabras
        public static int WordCount(this string str) {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        // Método de extensión para capitalizar
        public static string Capitalize(this string str) {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }

    public static class IntExtensions {
        // Método de extensión para verificar si es primo
        public static bool IsPrime(this int number) {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            
            for (int i = 3; i <= Math.Sqrt(number); i += 2) if (number % i == 0) return false;
            return true;
        }
    }

    class ExtensionMethodsExample {
        static void Main(string[] args) {
            string text = "Hola, mundo. ¿Cómo estás?";
            Console.WriteLine($"Texto: {text}");
            Console.WriteLine($"Número de palabras: {text.WordCount()}");
            Console.WriteLine($"Capitalizado: {text.Capitalize()}");

            int number = 17;
            Console.WriteLine($"{number} es primo: {number.IsPrime()}");

            number = 18;
            Console.WriteLine($"{number} es primo: {number.IsPrime()}");
        }
    }
}

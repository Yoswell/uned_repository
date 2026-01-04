/*== Genéricos en C# ==*/
using System;
using System.Collections.Generic;

namespace Uned.Temas.Generics {
    // Clase genérica
    class GenericList<T> {
        private List<T> items = new List<T>();

        public void Add(T item) => items.Add(item);

        public T Get(int index) {
            return items[index];
        }

        public int Count => items.Count;
    }

    // Método genérico
    class Utility {
        public static void Swap<T>(ref T a, ref T b) {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    class GenericsExample {
        static void Main(string[] args) {
            // Usar clase genérica
            GenericList<string> stringList = new GenericList<string>();
            stringList.Add("Hola");
            stringList.Add("Mundo");
            Console.WriteLine(stringList.Get(0)); // Hola

            // Usar método genérico
            int x = 5, y = 10;
            Utility.Swap(ref x, ref y);
            Console.WriteLine($"x: {x}, y: {y}"); // x: 10, y: 5
        }
    }
}

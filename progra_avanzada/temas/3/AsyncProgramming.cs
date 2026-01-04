/*== Programación Asíncrona en C# ==*/
using System;
using System.Threading.Tasks;

namespace Uned.Temas.AsyncProgramming {
    class AsyncExample {
        static async Task Main(string[] args) {
            Console.WriteLine("Inicio del programa");

            // Llamada asíncrona
            await DoWorkAsync();

            Console.WriteLine("Fin del programa");
        }

        static async Task DoWorkAsync() {
            Console.WriteLine("Trabajo asíncrono iniciado");

            // Simular trabajo asíncrono
            await Task.Delay(2000);

            Console.WriteLine("Trabajo asíncrono completado");
        }

        // Ejemplo con async/await y Task
        static async Task<int> CalculateAsync(int a, int b) {
            await Task.Delay(1000); // Simular cálculo
            return a + b;
        }
    }
}

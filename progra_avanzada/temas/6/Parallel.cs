/*== Tareas Paralelas en C# ==*/
using System;
using System.Threading.Tasks;

namespace Parallel {
    class ParallelExample {
        static void Main(string[] args) {
            // Tarea síncrona
            Console.WriteLine("Iniciando tarea síncrona...");
            DoWork("Síncrona");
            Console.WriteLine("Tarea síncrona completada.");

            // Tareas paralelas
            Task task1 = Task.Run(() => DoWork("Paralela 1"));
            Task task2 = Task.Run(() => DoWork("Paralela 2"));

            // Esperar a que terminen
            Task.WaitAll(task1, task2);
            Console.WriteLine("Todas las tareas paralelas completadas.");

            // Usar Parallel.For
            Console.WriteLine("Usando Parallel.For:");
            Parallel.For(0, 5, i => {
                Console.WriteLine($"Iteración paralela: {i}");
                Task.Delay(100).Wait();
            });

            // Usar Parallel.ForEach
            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.WriteLine("Usando Parallel.ForEach:");
            Parallel.ForEach(numbers, num => {
                Console.WriteLine($"Procesando: {num * 2}");
            });
        }

        static void DoWork(string name) {
            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"{name}: {i}");
                Task.Delay(200).Wait();
            }
        }
    }
}

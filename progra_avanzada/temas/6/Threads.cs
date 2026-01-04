/*== Hilos en C# ==*/
using System;
using System.Threading;

namespace Threads {
    class ThreadsExample {
        static void Main(string[] args) {
            // Crear y iniciar un hilo
            Thread thread1 = new Thread(PrintNumbers);
            thread1.Start("Hilo 1");

            // Otro hilo
            Thread thread2 = new Thread(PrintNumbers);
            thread2.Start("Hilo 2");

            // Hilo principal
            for (int i = 0; i < 5; i++) {
                Console.WriteLine($"Principal: {i}");
                Thread.Sleep(100);
            }

            // Esperar a que terminen
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Todos los hilos han terminado.");
        }

        static void PrintNumbers(object threadName) {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine($"{threadName}: {i}");
                Thread.Sleep(100);
            }
        }
    }
}

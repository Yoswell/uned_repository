/*== Manejo de Excepciones en C# ==*/
using System;

namespace Uned.Temas.ExceptionHandling {
    class ExceptionExample {
        static void Main(string[] args) {
            try {
                int result = Divide(10, 0);
                Console.WriteLine($"Resultado: {result}");
            } catch (DivideByZeroException ex) {
                Console.WriteLine($"Error: {ex.Message}");
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
            } finally {
                Console.WriteLine("Bloque finally ejecutado");
            }

            // Usar throw
            try {
                ValidateAge(-5);
            } catch (ArgumentException ex) {
                Console.WriteLine($"Validación fallida: {ex.Message}");
            }
        }

        static int Divide(int a, int b) {
            if (b == 0) throw new DivideByZeroException("No se puede dividir por cero");
            return a / b;
        }

        static void ValidateAge(int age) {
            if (age < 0) throw new ArgumentException("La edad no puede ser negativa");
        }
    }
}

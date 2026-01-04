/*== File I/O en C# ==*/
using System;
using System.IO;

namespace FileIO {
    class FileIOExample {
        static void Main(string[] args) {
            string filePath = "example.txt";

            // Escribir en un archivo
            using (StreamWriter writer = new StreamWriter(filePath)) {
                writer.WriteLine("Hola, mundo!");
                writer.WriteLine("Esto es un ejemplo de escritura en archivo.");
            }
            Console.WriteLine("Archivo escrito exitosamente.");

            // Leer desde un archivo
            if (File.Exists(filePath)) {
                using (StreamReader reader = new StreamReader(filePath)) {
                    string line;
                    Console.WriteLine("Contenido del archivo:");
                    while ((line = reader.ReadLine()) != null) Console.WriteLine(line);
                }
            } else {
                Console.WriteLine("El archivo no existe.");
            }

            // Agregar texto a un archivo
            using (StreamWriter writer = File.AppendText(filePath)) writer.WriteLine("Línea agregada.");
            Console.WriteLine("Texto agregado al archivo.");

            // Leer todo el archivo de una vez
            string content = File.ReadAllText(filePath);
            Console.WriteLine("Contenido completo:");
            Console.WriteLine(content);

            // Limpiar: eliminar el archivo
            File.Delete(filePath);
            Console.WriteLine("Archivo eliminado.");
        }
    }
}

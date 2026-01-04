/*== Interpolación de strings, manejo de variables dentro de strings ==*/
using System;

namespace Particularities {
    public class Configuration {
        private string configFile = "apache.config";
        private string filePath = @"C:\Users\Apache";
        private string mainPath;

        static void Main(string[] args) {
            Configuration config = new Configuration();

            // Forma tradicional de concatenación
            config.mainPath = config.filePath + "\\" + config.configFile;
            Console.WriteLine("Ruta tradicional: " + config.mainPath);

            // Forma moderna usando interpolación
            config.mainPath = $"{config.filePath}\\{config.configFile}";
            Console.WriteLine("Ruta con interpolación: " + config.mainPath);

            // Interpolación con verbatim strings
            string multiLinePath = $@"{config.filePath}\{config.configFile}";
            Console.WriteLine("Ruta verbatim: " + multiLinePath);

            // Más ejemplos de interpolación
            int version = 2;
            string extension = ".bak";
            string backupPath = $"{config.filePath}\\{config.configFile}.v{version}{extension}";
            Console.WriteLine("Ruta de respaldo: " + backupPath);

            // Interpolación con expresiones
            DateTime now = DateTime.Now;
            string logMessage = $"[{now:yyyy-MM-dd HH:mm:ss}] Archivo procesado: {config.configFile.ToUpper()}";
            Console.WriteLine("Mensaje de log: " + logMessage);

            // Interpolación con formato numérico
            double size = 1024.567;
            string sizeInfo = $"Tamaño del archivo: {size:N2} KB";
            Console.WriteLine(sizeInfo);

            // Interpolación con condicionales
            bool isValid = config.configFile.EndsWith(".config");
            string validationMessage = $"El archivo {config.configFile} {(isValid ? "es válido" : "no es válido")}";
            Console.WriteLine(validationMessage);
        }
    }
}

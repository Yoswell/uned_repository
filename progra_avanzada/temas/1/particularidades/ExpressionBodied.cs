/*== Expression Bodied Members (miembros con cuerpo de expresión) ==*/
using System;

namespace Particularities {
    public class ExpressionBodiedExample {
        private string configFile;
        private bool fileDeleted;

        // Constructor tradicional
        public ExpressionBodiedExample(string file) {
            this.configFile = file;
            this.fileDeleted = false;
        }

        // Expression Bodied constructor con tuplas
        public ExpressionBodiedExample(string file, bool deleted) => (this.configFile, this.fileDeleted) = (file, deleted);

        // Propiedad con getter expression bodied
        public string ConfigFile => configFile;

        // Propiedad con getter y setter expression bodied
        public bool FileDeleted {
            get => fileDeleted;
            set => fileDeleted = value;
        }

        // Método expression bodied
        public string GetFileInfo() => $"Archivo: {configFile}, Eliminado: {fileDeleted}";

        // Método con operaciones
        public string GetFullPath() => Path.Combine(Environment.CurrentDirectory, configFile);

        // Método estático expression bodied
        public static string GetDefaultPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    class Program {
        static void Main(string[] args) {
            // Usar constructor tradicional
            ExpressionBodiedExample example1 = new ExpressionBodiedExample("config.txt");
            Console.WriteLine(example1.GetFileInfo());

            // Usar constructor expression bodied
            ExpressionBodiedExample example2 = new ExpressionBodiedExample("data.txt", true);
            Console.WriteLine(example2.GetFileInfo());

            // Usar propiedades
            Console.WriteLine($"Archivo de config: {example1.ConfigFile}");
            Console.WriteLine($"¿Archivo eliminado? {example2.FileDeleted}");

            // Usar métodos
            Console.WriteLine($"Ruta completa: {example1.GetFullPath()}");
            Console.WriteLine($"Ruta por defecto: {ExpressionBodiedExample.GetDefaultPath()}");
        }
    }
}

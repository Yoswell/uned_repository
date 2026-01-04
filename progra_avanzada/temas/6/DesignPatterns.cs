/*== Patrones de Diseño: Singleton y Factory en C# ==*/
using System;

namespace DesignPatterns {
    // Patrón Singleton
    class Singleton {
        private static Singleton instance;
        private static readonly object lockObject = new object();

        private Singleton() { }

        public static Singleton Instance {
            get {
                lock (lockObject) {
                    if (instance == null) instance = new Singleton();
                    return instance;
                }
            }
        }

        public void ShowMessage() => Console.WriteLine("Mensaje desde Singleton.");
    }

    // Patrón Factory
    interface IShape {
        void Draw();
    }

    class Circle : IShape {
        public void Draw() => Console.WriteLine("Dibujando un círculo.");
    }

    class Rectangle : IShape {
        public void Draw() => Console.WriteLine("Dibujando un rectángulo.");
    }

    class ShapeFactory {
        public IShape CreateShape(string shapeType) {
            switch (shapeType.ToLower()) {
                case "circle":
                    return new Circle();
                case "rectangle":
                    return new Rectangle();
                default:
                    throw new ArgumentException("Tipo de forma desconocido.");
            }
        }
    }

    class DesignPatternsExample {
        static void Main(string[] args) {
            // Usar Singleton
            Singleton s1 = Singleton.Instance;
            Singleton s2 = Singleton.Instance;
            Console.WriteLine($"¿Son la misma instancia? {s1 == s2}");
            s1.ShowMessage();

            // Usar Factory
            ShapeFactory factory = new ShapeFactory();
            IShape circle = factory.CreateShape("circle");
            IShape rectangle = factory.CreateShape("rectangle");

            circle.Draw();
            rectangle.Draw();
        }
    }
}

/*== Polimorfismo en C# ==*/
using System;

namespace Polymorphism {
    // Clase base
    class Shape {
        public virtual void Draw() => Console.WriteLine("Dibujando una forma.");

        public virtual double Area() {
            return 0;
        }
    }

    // Clase derivada
    class Circle : Shape {
        public double Radius { get; set; }

        public Circle(double radius) => Radius = radius;

        public override void Draw() => Console.WriteLine("Dibujando un círculo.");

        public override double Area() {
            return Math.PI * Radius * Radius;
        }
    }

    // Otra clase derivada
    class Rectangle : Shape {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height) {
            Width = width;
            Height = height;
        }

        public override void Draw() => Console.WriteLine("Dibujando un rectángulo.");

        public override double Area() {
            return Width * Height;
        }
    }

    class PolymorphismExample {
        static void Main(string[] args) {
            // Crear instancias
            Shape circle = new Circle(5);
            Shape rectangle = new Rectangle(4, 6);

            // Polimorfismo: llamar métodos sobreescritos
            circle.Draw();
            Console.WriteLine($"Área del círculo: {circle.Area()}");

            rectangle.Draw();
            Console.WriteLine($"Área del rectángulo: {rectangle.Area()}");

            // Array de formas
            Shape[] shapes = { circle, rectangle };
            Console.WriteLine("Áreas de todas las formas:");
            foreach (Shape shape in shapes) {
                Console.WriteLine($"{shape.Area()}");
            }
        }
    }
}

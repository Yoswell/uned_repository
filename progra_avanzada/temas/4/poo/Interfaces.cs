/*== Interfaces en C# ==*/
using System;

namespace Interfaces {
    // Definir una interfaz
    interface IDrawable {
        void Draw();
    }

    interface IShape {
        double Area();
        double Perimeter();
    }

    // Clase que implementa interfaces
    class Circle : IDrawable, IShape {
        public double Radius { get; set; }

        public Circle(double radius) => Radius = radius;

        public void Draw() => Console.WriteLine("Dibujando un círculo.");

        public double Area() {
            return Math.PI * Radius * Radius;
        }

        public double Perimeter() {
            return 2 * Math.PI * Radius;
        }
    }

    // Otra clase que implementa interfaces
    class Rectangle : IDrawable, IShape {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height) {
            Width = width;
            Height = height;
        }

        public void Draw() => Console.WriteLine("Dibujando un rectángulo.");

        public double Area() {
            return Width * Height;
        }

        public double Perimeter() {
            return 2 * (Width + Height);
        }
    }

    class InterfacesExample {
        static void Main(string[] args) {
            // Usar interfaces
            IDrawable drawable1 = new Circle(5);
            IDrawable drawable2 = new Rectangle(4, 6);

            drawable1.Draw();
            drawable2.Draw();

            IShape shape1 = (IShape)drawable1;
            IShape shape2 = (IShape)drawable2;

            Console.WriteLine($"Área del círculo: {shape1.Area()}, Perímetro: {shape1.Perimeter()}");
            Console.WriteLine($"Área del rectángulo: {shape2.Area()}, Perímetro: {shape2.Perimeter()}");
        }
    }
}

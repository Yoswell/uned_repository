/*== Herencia en C# ==*/
using System;

namespace Inheritance {
    // Clase base
    class Animal {
        public string Name { get; set; }

        public void Eat() => Console.WriteLine($"{Name} está comiendo.");

        public virtual void MakeSound() => Console.WriteLine($"{Name} hace un sonido.");
    }

    // Clase derivada
    class Dog : Animal {
        public void Bark() => Console.WriteLine($"{Name} ladra.");

        public override void MakeSound() => Console.WriteLine($"{Name} ladra: ¡Guau!");
    }

    // Otra clase derivada
    class Cat : Animal {
        public void Meow() => Console.WriteLine($"{Name} maúlla.");

        public override void MakeSound() => Console.WriteLine($"{Name} maúlla: ¡Miau!");
    }

    class InheritanceExample {
        static void Main(string[] args) {
            // Crear instancias de clases derivadas
            Dog dog = new Dog { Name = "Rex" };
            Cat cat = new Cat { Name = "Whiskers" };

            // Usar métodos heredados
            dog.Eat();
            dog.MakeSound();
            dog.Bark();

            cat.Eat();
            cat.MakeSound();
            cat.Meow();

            // Polimorfismo: tratar derivadas como base
            Animal[] animals = { dog, cat };
            foreach (Animal animal in animals) {
                animal.MakeSound();
            }
        }
    }
}

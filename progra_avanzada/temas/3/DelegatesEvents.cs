/*== Delegados y Eventos en C# ==*/
using System;

namespace Uned.Temas.DelegatesEvents {
    // Definir un delegado
    public delegate void Notify(string message);

    class Publisher {
        // Evento basado en el delegado
        public event Notify OnNotify;

        public void DoSomething() {
            Console.WriteLine("Haciendo algo...");
            // Disparar el evento
            OnNotify?.Invoke("Algo sucedió!");
        }
    }

    class Subscriber {
        public void HandleNotification(string message) => Console.WriteLine($"Notificación recibida: {message}");
    }

    class DelegatesEventsExample {
        static void Main(string[] args) {
            Publisher pub = new Publisher();
            Subscriber sub = new Subscriber();

            // Suscribir al evento
            pub.OnNotify += sub.HandleNotification;

            pub.DoSomething();

            // Desuscribir
            pub.OnNotify -= sub.HandleNotification;
        }
    }
}

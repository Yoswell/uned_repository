/*== Creación de múltiples clases y uso de namespaces ==*/
using System;

namespace Companies {
    // Múltiples clases en el mismo namespace
    class Intelec {
        public string Location { get; set; }

        public void DisplayInfo() => Console.WriteLine($"Intelec - Ubicación: {Location}");
    }

    class Techzilla {
        public string Location { get; set; }

        public void DisplayInfo() => Console.WriteLine($"Techzilla - Ubicación: {Location}");
    }

    class ExtremeTech {
        public string Location { get; set; }

        public void DisplayInfo() => Console.WriteLine($"ExtremeTech - Ubicación: {Location}");
    }
}

// La misma clase en diferentes namespaces
namespace SanJose {
    class Intelec {
        public string Sales { get; set; }

        public void DisplayInfo() => Console.WriteLine($"Intelec San José - Ventas: {Sales}");
    }
}

namespace Heredia {
    class Intelec {
        public string Sales { get; set; }

        public void DisplayInfo() => Console.WriteLine($"Intelec Heredia - Ventas: {Sales}");
    }
}

namespace Particularities {
    class ClassExample {
        static void Main(string[] args) {
            // Usar clases del namespace Companies
            Companies.Intelec intelec = new Companies.Intelec { Location = "San José" };
            intelec.DisplayInfo();

            Companies.Techzilla techzilla = new Companies.Techzilla { Location = "Heredia" };
            techzilla.DisplayInfo();

            // Usar clases con el mismo nombre en diferentes namespaces
            SanJose.Intelec intelecSJ = new SanJose.Intelec { Sales = "$100,000" };
            intelecSJ.DisplayInfo();

            Heredia.Intelec intelecH = new Heredia.Intelec { Sales = "$80,000" };
            intelecH.DisplayInfo();
        }
    }
}

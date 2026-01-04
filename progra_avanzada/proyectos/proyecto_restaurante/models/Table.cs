using System;

namespace ProyectoRestaurante.Models {
    public class Table {
        public int TableID { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; } // e.g., Window, Center
        public bool IsOccupied { get; set; }
    }
}

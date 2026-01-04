using System;

namespace ProyectoRestaurante.Models {
    public class MenuItem {
        public int MenuItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } // e.g., Appetizer, Main, Dessert
        public bool IsAvailable { get; set; }
    }
}

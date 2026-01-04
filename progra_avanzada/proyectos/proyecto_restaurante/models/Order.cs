using System;

namespace ProyectoRestaurante.Models {
    public class Order {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? TableID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // e.g., Pending, Preparing, Served, Paid

        public Customer Customer { get; set; }
        public Table Table { get; set; }
    }
}

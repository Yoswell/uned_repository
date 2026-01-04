using System;

namespace ProyectoRestaurante.Models {
    public class Customer {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}

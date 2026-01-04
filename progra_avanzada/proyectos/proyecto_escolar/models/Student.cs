using System;

namespace ProyectoEscolar.models {
    public class Student {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}

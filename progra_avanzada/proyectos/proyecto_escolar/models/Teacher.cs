using System;

namespace ProyectoEscolar.models {
    public class Teacher {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialty { get; set; }
        public DateTime HireDate { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}

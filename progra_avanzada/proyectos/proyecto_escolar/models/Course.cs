namespace ProyectoEscolar.models {
    public class Course {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}

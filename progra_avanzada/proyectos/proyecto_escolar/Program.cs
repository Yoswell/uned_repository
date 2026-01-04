using System;
using ProyectoEscolar.DataAccess;
using ProyectoEscolar.Models;
using ProyectoEscolar.Services;

namespace ProyectoEscolar {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== Sistema de Gestión Escolar ===");

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Menú Principal:");
                Console.WriteLine("1. Gestionar Estudiantes");
                Console.WriteLine("2. Gestionar Cursos");
                Console.WriteLine("3. Gestionar Matrículas");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        ManageStudents();
                        break;
                    case "2":
                        ManageCourses();
                        break;
                    case "3":
                        ManageEnrollments();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void ManageStudents() {
            StudentRepository repo = new StudentRepository();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Estudiantes:");
                Console.WriteLine("1. Ver todos los estudiantes");
                Console.WriteLine("2. Agregar estudiante");
                Console.WriteLine("3. Actualizar estudiante");
                Console.WriteLine("4. Eliminar estudiante");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        var students = repo.GetAllStudents();
                        foreach (var student in students) {
                            Console.WriteLine($"{student.StudentID}: {student.FullName} - {student.Email}");
                        }
                        break;
                    case "2":
                        AddStudent(repo);
                        break;
                    case "3":
                        UpdateStudent(repo);
                        break;
                    case "4":
                        DeleteStudent(repo);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AddStudent(StudentRepository repo) {
            Console.Write("Nombre: ");
            string firstName = Console.ReadLine();
            Console.Write("Apellido: ");
            string lastName = Console.ReadLine();
            Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
            DateTime dob = DateTime.Parse(Console.ReadLine());
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Teléfono: ");
            string phone = Console.ReadLine();
            Console.Write("Dirección: ");
            string address = Console.ReadLine();

            Student student = new Student {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dob,
                Email = email,
                Phone = phone,
                Address = address
            };

            try {
                repo.AddStudent(student);
                Console.WriteLine("Estudiante agregado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateStudent(StudentRepository repo) {
            Console.Write("ID del estudiante: ");
            int id = int.Parse(Console.ReadLine());
            Student student = repo.GetStudentById(id);
            if (student == null) {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.Write($"Nombre ({student.FirstName}): ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrEmpty(firstName)) student.FirstName = firstName;

            Console.Write($"Apellido ({student.LastName}): ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(lastName)) student.LastName = lastName;

            // Similar para otros campos...

            try {
                repo.UpdateStudent(student);
                Console.WriteLine("Estudiante actualizado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteStudent(StudentRepository repo) {
            Console.Write("ID del estudiante: ");
            int id = int.Parse(Console.ReadLine());
            try {
                repo.DeleteStudent(id);
                Console.WriteLine("Estudiante eliminado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ManageCourses() {
            CourseRepository repo = new CourseRepository();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Cursos:");
                Console.WriteLine("1. Ver todos los cursos");
                Console.WriteLine("2. Agregar curso");
                Console.WriteLine("3. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        var courses = repo.GetAllCourses();
                        foreach (var course in courses) {
                            string teacherName = course.Teacher?.FullName ?? "Sin profesor";
                            Console.WriteLine($"{course.CourseID}: {course.Name} - {course.Credits} créditos - {teacherName}");
                        }
                        break;
                    case "2":
                        AddCourse(repo);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AddCourse(CourseRepository repo) {
            Console.Write("Nombre del curso: ");
            string name = Console.ReadLine();
            Console.Write("Descripción: ");
            string description = Console.ReadLine();
            Console.Write("Créditos: ");
            int credits = int.Parse(Console.ReadLine());
            Console.Write("ID del profesor (opcional): ");
            string teacherIdStr = Console.ReadLine();
            int? teacherId = string.IsNullOrEmpty(teacherIdStr) ? null : int.Parse(teacherIdStr);

            Course course = new Course {
                Name = name,
                Description = description,
                Credits = credits,
                TeacherID = teacherId
            };

            try {
                repo.AddCourse(course);
                Console.WriteLine("Curso agregado exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ManageEnrollments() {
            EnrollmentService service = new EnrollmentService();

            while (true) {
                Console.WriteLine();
                Console.WriteLine("Gestión de Matrículas:");
                Console.WriteLine("1. Matricular estudiante");
                Console.WriteLine("2. Ver matrículas de estudiante");
                Console.WriteLine("3. Asignar calificación");
                Console.WriteLine("4. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option) {
                    case "1":
                        Console.Write("ID del estudiante: ");
                        int studentId = int.Parse(Console.ReadLine());
                        Console.Write("ID del curso: ");
                        int courseId = int.Parse(Console.ReadLine());
                        service.EnrollStudent(studentId, courseId);
                        break;
                    case "2":
                        Console.Write("ID del estudiante: ");
                        int sid = int.Parse(Console.ReadLine());
                        service.DisplayStudentEnrollments(sid);
                        break;
                    case "3":
                        Console.Write("ID de la matrícula: ");
                        int enrollmentId = int.Parse(Console.ReadLine());
                        Console.Write("Calificación: ");
                        decimal grade = decimal.Parse(Console.ReadLine());
                        service.AssignGrade(enrollmentId, grade);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }
    }
}

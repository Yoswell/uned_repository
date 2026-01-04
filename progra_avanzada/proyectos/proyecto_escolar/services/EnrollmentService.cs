using System;
using System.Collections.Generic;
using ProyectoEscolar.DataAccess;
using ProyectoEscolar.Models;

namespace ProyectoEscolar.services {
    public class EnrollmentService {
        private StudentRepository studentRepo = new StudentRepository();
        private CourseRepository courseRepo = new CourseRepository();
        private EnrollmentRepository enrollmentRepo = new EnrollmentRepository();

        public bool EnrollStudent(int studentID, int courseID) {
            try {
                // Verificar que el estudiante existe
                Student student = studentRepo.GetStudentById(studentID);
                if (student == null) {
                    Console.WriteLine("Estudiante no encontrado.");
                    return false;
                }

                // Verificar que el curso existe
                Course course = courseRepo.GetCourseById(courseID);
                if (course == null) {
                    Console.WriteLine("Curso no encontrado.");
                    return false;
                }

                // Verificar que no esté ya matriculado
                List<Enrollment> existingEnrollments = enrollmentRepo.GetAllEnrollments();
                foreach (var enrollment in existingEnrollments) {
                    if (enrollment.StudentID == studentID && enrollment.CourseID == courseID) {
                        Console.WriteLine("El estudiante ya está matriculado en este curso.");
                        return false;
                    }
                }

                // Matricular
                Enrollment newEnrollment = new Enrollment {
                    StudentID = studentID,
                    CourseID = courseID,
                    EnrollmentDate = DateTime.Now
                };
                enrollmentRepo.AddEnrollment(newEnrollment);
                Console.WriteLine("Estudiante matriculado exitosamente.");
                return true;
            } catch (Exception ex) {
                Console.WriteLine($"Error al matricular estudiante: {ex.Message}");
                return false;
            }
        }

        public void DisplayStudentEnrollments(int studentID) {
            Student student = studentRepo.GetStudentById(studentID);
            if (student == null) {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.WriteLine($"Matriculas de {student.FullName}:");
            List<Enrollment> enrollments = enrollmentRepo.GetAllEnrollments();
            foreach (var enrollment in enrollments) {
                if (enrollment.StudentID == studentID) Console.WriteLine($"- {enrollment.Course.Name} (Calificación: {enrollment.Grade ?? 0})");
            }
        }

        public void AssignGrade(int enrollmentID, decimal grade) {
            try {
                enrollmentRepo.UpdateGrade(enrollmentID, grade);
                Console.WriteLine("Calificación asignada exitosamente.");
            } catch (Exception ex) {
                Console.WriteLine($"Error al asignar calificación: {ex.Message}");
            }
        }
    }
}

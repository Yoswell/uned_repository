using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoEscolar.Models;

namespace ProyectoEscolar.dataAccess {
    public class EnrollmentRepository {
        public List<Enrollment> GetAllEnrollments() {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"SELECT m.*, e.Nombre as StudentName, e.Apellido as StudentLastName,
                                c.Nombre as CourseName
                                FROM Matriculas m
                                JOIN Estudiantes e ON m.EstudianteID = e.EstudianteID
                                JOIN Cursos c ON m.CursoID = c.CursoID";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    enrollments.Add(new Enrollment {
                        EnrollmentID = (int)reader["MatriculaID"],
                        StudentID = (int)reader["EstudianteID"],
                        CourseID = (int)reader["CursoID"],
                        EnrollmentDate = (DateTime)reader["FechaMatricula"],
                        Grade = reader["Calificacion"] as decimal?,
                        Student = new Student {
                            StudentID = (int)reader["EstudianteID"],
                            FirstName = reader["StudentName"].ToString(),
                            LastName = reader["StudentLastName"].ToString()
                        },
                        Course = new Course {
                            CourseID = (int)reader["CursoID"],
                            Name = reader["CourseName"].ToString()
                        }
                    });
                }
            }
            return enrollments;
        }

        public void AddEnrollment(Enrollment enrollment) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO Matriculas (EstudianteID, CursoID, Calificacion)
                                VALUES (@studentID, @courseID, @grade)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentID", enrollment.StudentID);
                cmd.Parameters.AddWithValue("@courseID", enrollment.CourseID);
                cmd.Parameters.AddWithValue("@grade", enrollment.Grade ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGrade(int enrollmentID, decimal grade) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "UPDATE Matriculas SET Calificacion = @grade WHERE MatriculaID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", enrollmentID);
                cmd.Parameters.AddWithValue("@grade", grade);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEnrollment(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM Matriculas WHERE MatriculaID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

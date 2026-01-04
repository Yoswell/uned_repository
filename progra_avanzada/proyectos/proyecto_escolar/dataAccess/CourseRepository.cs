using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoEscolar.Models;

namespace ProyectoEscolar.dataAccess {
    public class CourseRepository {
        public List<Course> GetAllCourses() {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"SELECT c.*, t.Nombre as TeacherName, t.Apellido as TeacherLastName
                                FROM Cursos c LEFT JOIN Profesores t ON c.ProfesorID = t.ProfesorID";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Course course = new Course {
                        CourseID = (int)reader["CursoID"],
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Credits = (int)reader["Creditos"],
                        TeacherID = reader["ProfesorID"] as int?
                    };
                    if (course.TeacherID.HasValue) {
                        course.Teacher = new Teacher {
                            TeacherID = course.TeacherID.Value,
                            FirstName = reader["TeacherName"].ToString(),
                            LastName = reader["TeacherLastName"].ToString()
                        };
                    }
                    courses.Add(course);
                }
            }
            return courses;
        }

        public Course GetCourseById(int id) {
            Course course = null;
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"SELECT c.*, t.Nombre as TeacherName, t.Apellido as TeacherLastName
                                FROM Cursos c LEFT JOIN Profesores t ON c.ProfesorID = t.ProfesorID
                                WHERE c.CursoID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    course = new Course {
                        CourseID = (int)reader["CursoID"],
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Credits = (int)reader["Creditos"],
                        TeacherID = reader["ProfesorID"] as int?
                    };
                    if (course.TeacherID.HasValue) {
                        course.Teacher = new Teacher {
                            TeacherID = course.TeacherID.Value,
                            FirstName = reader["TeacherName"].ToString(),
                            LastName = reader["TeacherLastName"].ToString()
                        };
                    }
                }
            }
            return course;
        }

        public void AddCourse(Course course) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO Cursos (Nombre, Descripcion, Creditos, ProfesorID)
                                VALUES (@nombre, @descripcion, @creditos, @profesorID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", course.Name);
                cmd.Parameters.AddWithValue("@descripcion", course.Description);
                cmd.Parameters.AddWithValue("@creditos", course.Credits);
                cmd.Parameters.AddWithValue("@profesorID", course.TeacherID ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCourse(Course course) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"UPDATE Cursos SET Nombre = @nombre, Descripcion = @descripcion,
                                Creditos = @creditos, ProfesorID = @profesorID WHERE CursoID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", course.CourseID);
                cmd.Parameters.AddWithValue("@nombre", course.Name);
                cmd.Parameters.AddWithValue("@descripcion", course.Description);
                cmd.Parameters.AddWithValue("@creditos", course.Credits);
                cmd.Parameters.AddWithValue("@profesorID", course.TeacherID ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCourse(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM Cursos WHERE CursoID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

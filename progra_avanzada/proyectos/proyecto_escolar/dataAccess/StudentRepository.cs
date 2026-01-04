using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProyectoEscolar.Models;

namespace ProyectoEscolar.dataAccess {
    public class StudentRepository {
        public List<Student> GetAllStudents() {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Estudiantes";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    students.Add(new Student {
                        StudentID = (int)reader["EstudianteID"],
                        FirstName = reader["Nombre"].ToString(),
                        LastName = reader["Apellido"].ToString(),
                        DateOfBirth = (DateTime)reader["FechaNacimiento"],
                        Email = reader["Email"].ToString(),
                        Phone = reader["Telefono"].ToString(),
                        Address = reader["Direccion"].ToString(),
                        RegistrationDate = (DateTime)reader["FechaRegistro"]
                    });
                }
            }
            return students;
        }

        public Student GetStudentById(int id) {
            Student student = null;
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "SELECT * FROM Estudiantes WHERE EstudianteID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    student = new Student {
                        StudentID = (int)reader["EstudianteID"],
                        FirstName = reader["Nombre"].ToString(),
                        LastName = reader["Apellido"].ToString(),
                        DateOfBirth = (DateTime)reader["FechaNacimiento"],
                        Email = reader["Email"].ToString(),
                        Phone = reader["Telefono"].ToString(),
                        Address = reader["Direccion"].ToString(),
                        RegistrationDate = (DateTime)reader["FechaRegistro"]
                    };
                }
            }
            return student;
        }

        public void AddStudent(Student student) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"INSERT INTO Estudiantes (Nombre, Apellido, FechaNacimiento, Email, Telefono, Direccion)
                                VALUES (@nombre, @apellido, @fechaNac, @email, @telefono, @direccion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", student.FirstName);
                cmd.Parameters.AddWithValue("@apellido", student.LastName);
                cmd.Parameters.AddWithValue("@fechaNac", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@telefono", student.Phone);
                cmd.Parameters.AddWithValue("@direccion", student.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = @"UPDATE Estudiantes SET Nombre = @nombre, Apellido = @apellido,
                                FechaNacimiento = @fechaNac, Email = @email, Telefono = @telefono,
                                Direccion = @direccion WHERE EstudianteID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", student.StudentID);
                cmd.Parameters.AddWithValue("@nombre", student.FirstName);
                cmd.Parameters.AddWithValue("@apellido", student.LastName);
                cmd.Parameters.AddWithValue("@fechaNac", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@telefono", student.Phone);
                cmd.Parameters.AddWithValue("@direccion", student.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id) {
            using (SqlConnection conn = DatabaseHelper.GetConnection()) {
                string query = "DELETE FROM Estudiantes WHERE EstudianteID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WCFService
{
    public class CourseService : IService
    {
        private string connectionString = "Server=localhost;Database=CourseManagementDB;Uid=root;Pwd=your_password;";

        public string RegisterStudent(string name, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Students (Name, Email) VALUES (@Name, @Email); SELECT LAST_INSERT_ID();", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    return string.Format("Student {0} registered with ID {1}.", name, newId);
                }
            }
        }

        public List<string> GetAvailableCourses()
        {
            List<string> courses = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT Name FROM Courses", conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(reader["Name"].ToString());
                        }
                    }
                }
            }
            return courses;
        }

        public bool EnrollStudent(int studentId, int courseId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO Enrollments (StudentId, CourseId) VALUES (@StudentId, @CourseId)", conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error enrolling student: " + ex.Message);
                return false;
            }
        }

        public List<string> GetStudentCourses(int studentId)
        {
            List<string> enrolledCourses = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(
                    @"SELECT c.Name 
                      FROM Courses c 
                      JOIN Enrollments e ON c.Id = e.CourseId 
                      WHERE e.StudentId = @StudentId", conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            enrolledCourses.Add(reader["Name"].ToString());
                        }
                    }
                }
            }
            return enrolledCourses;
        }
    }
}

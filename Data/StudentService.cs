using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace class_organizer.Data
{
    public class StudentService
    {
        public string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=Database.mdf;Integrated Security=True";

        [HttpPost]
        public void Create(string student_name, string student_lastname, string student_email, int student_phone, string student_obligation, string student_level)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Student (student_name, student_lastname, student_email, student_phone, student_obligation, student_level) VALUS (@student_name, @student_lastname, @student_email, @student_phone, @student_obligation, @student_level)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@student_name", student_name);
                command.Parameters.AddWithValue("@student_lastname", student_lastname);
                command.Parameters.AddWithValue("@student_email", student_email);
                command.Parameters.AddWithValue("@student_phone", student_phone);
                command.Parameters.AddWithValue("@student_obligation", student_obligation);
                command.Parameters.AddWithValue("@student_level", student_level);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public List<StudentModel> GetStudents()
        {
            // Return a list of students (mock or from a database)
            return new List<StudentModel>();
        }
    }
}

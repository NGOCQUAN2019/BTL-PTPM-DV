using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Data;

namespace DAL
{
    // Interface
    public interface IStudentRepository
    {
        bool CreateStudent(StudentModel model);
    }

    // Class thực thi
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CreateStudent(StudentModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_create_student", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số khớp với Stored Procedure sp_create_student
                    cmd.Parameters.AddWithValue("@p_student_code", model.StudentCode);
                    cmd.Parameters.AddWithValue("@p_full_name", model.FullName);
                    cmd.Parameters.AddWithValue("@p_email", model.Email);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu Insert thành công
                }
            }
        }
    }
}
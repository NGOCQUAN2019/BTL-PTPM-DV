using DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CreateStudent(StudentModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_student_code = model.StudentCode, p_full_name = model.FullName, p_email = model.Email };
                db.Execute("sp_create_student", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool UpdateStudent(StudentModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_student_code = model.StudentCode, p_full_name = model.FullName, p_email = model.Email };
                db.Execute("sp_update_student", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool DeleteStudent(string studentCode)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_student_code = studentCode };
                db.Execute("sp_delete_student", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public StudentModel GetStudentById(string studentCode)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_student_code = studentCode };
                // QueryFirstOrDefault trả về 1 object duy nhất hoặc null nếu không tìm thấy
                return db.QueryFirstOrDefault<StudentModel>("sp_get_student_by_id", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<StudentModel> SearchStudents(string keyword)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_keyword = keyword ?? "" };
                return db.Query<StudentModel>("sp_search_students", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
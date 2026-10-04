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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CreateTeacher(TeacherModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    p_teacher_code = model.TeacherCode,
                    p_full_name = model.FullName,
                    p_phone = model.Phone
                };

                db.Execute("sp_create_teacher", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public List<TeacherModel> SearchTeachers(string keyword)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_keyword = keyword ?? "" };
                return db.Query<TeacherModel>("sp_search_teachers", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
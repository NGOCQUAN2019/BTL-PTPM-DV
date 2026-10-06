using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string _conn;

        public CourseRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<CourseModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT id AS Id, code AS Code, name AS Name, level AS Level, 
                                      duration_hours AS DurationHours, tuition_fee AS TuitionFee, 
                                      requires_placement AS RequiresPlacement, pass_score AS PassScore, 
                                      min_attendance_pct AS MinAttendancePct 
                               FROM courses";
                return db.Query<CourseModel>(sql);
            }
        }

        public CourseModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT id AS Id, code AS Code, name AS Name, level AS Level, 
                                      duration_hours AS DurationHours, tuition_fee AS TuitionFee, 
                                      requires_placement AS RequiresPlacement, pass_score AS PassScore, 
                                      min_attendance_pct AS MinAttendancePct 
                               FROM courses WHERE id = @Id";
                return db.QueryFirstOrDefault<CourseModel>(sql, new { Id = id });
            }
        }

        public bool Create(CourseModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"INSERT INTO courses (code, name, level, duration_hours, tuition_fee, requires_placement, pass_score, min_attendance_pct) 
                               VALUES (@Code, @Name, @Level, @DurationHours, @TuitionFee, @RequiresPlacement, @PassScore, @MinAttendancePct)";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Update(CourseModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"UPDATE courses 
                               SET code = @Code, name = @Name, level = @Level, duration_hours = @DurationHours, 
                                   tuition_fee = @TuitionFee, requires_placement = @RequiresPlacement, 
                                   pass_score = @PassScore, min_attendance_pct = @MinAttendancePct 
                               WHERE id = @Id";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Execute("DELETE FROM courses WHERE id = @Id", new { Id = id }) > 0;
            }
        }
    }
}
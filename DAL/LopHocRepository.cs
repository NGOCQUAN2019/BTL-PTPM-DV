using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class LopHocRepository : ILopHocRepository
    {
        private readonly string _conn;

        public LopHocRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<LopHocModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT id AS Id, class_code AS ClassCode, course_id AS CourseId, 
                                      teacher_id AS TeacherId, room AS Room, weekdays AS Weekdays, 
                                      start_time AS StartTime, end_time AS EndTime, 
                                      start_date AS StartDate, end_date AS EndDate, 
                                      max_students AS MaxStudents, status AS Status 
                               FROM classes";
                return db.Query<LopHocModel>(sql);
            }
        }

        public LopHocModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT id AS Id, class_code AS ClassCode, course_id AS CourseId, 
                                      teacher_id AS TeacherId, room AS Room, weekdays AS Weekdays, 
                                      start_time AS StartTime, end_time AS EndTime, 
                                      start_date AS StartDate, end_date AS EndDate, 
                                      max_students AS MaxStudents, status AS Status 
                               FROM classes WHERE id = @Id";
                return db.QueryFirstOrDefault<LopHocModel>(sql, new { Id = id });
            }
        }

        public bool Create(LopHocModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"INSERT INTO classes (class_code, course_id, teacher_id, room, weekdays, start_time, end_time, start_date, end_date, max_students, status) 
                               VALUES (@ClassCode, @CourseId, @TeacherId, @Room, @Weekdays, @StartTime, @EndTime, @StartDate, @EndDate, @MaxStudents, @Status)";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Update(LopHocModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"UPDATE classes 
                               SET class_code = @ClassCode, course_id = @CourseId, teacher_id = @TeacherId, 
                                   room = @Room, weekdays = @Weekdays, start_time = @StartTime, 
                                   end_time = @EndTime, start_date = @StartDate, end_date = @EndDate, 
                                   max_students = @MaxStudents, status = @Status 
                               WHERE id = @Id";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Execute("DELETE FROM classes WHERE id = @Id", new { Id = id }) > 0;
            }
        }
    }
}
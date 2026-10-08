using DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Data;

namespace DAL
{
    public class GiaoVienRepository : IGiaoVienRepository
    {
        private readonly string _conn;

        public GiaoVienRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<GiaoVienModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT id AS Id, user_id AS UserId, teacher_code AS TeacherCode, 
                                      full_name AS FullName, phone AS Phone, gender AS Gender 
                               FROM teachers";
                return db.Query<GiaoVienModel>(sql);
            }
        }

        public GiaoVienModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT id AS Id, user_id AS UserId, teacher_code AS TeacherCode, 
                                      full_name AS FullName, phone AS Phone, gender AS Gender 
                               FROM teachers WHERE id = @Id";
                return db.QueryFirstOrDefault<GiaoVienModel>(sql, new { Id = id });
            }
        }

        public bool Create(GiaoVienModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Lệnh Insert đơn giản, Trigger dưới SQL sẽ tự lo phần tạo User và Password
                string sql = @"INSERT INTO teachers (teacher_code, full_name, phone, gender) 
                               VALUES (@TeacherCode, @FullName, @Phone, @Gender)";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Update(GiaoVienModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"UPDATE teachers 
                               SET full_name = @FullName, phone = @Phone, gender = @Gender 
                               WHERE id = @Id";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Lưu ý: Để xóa hoàn toàn giáo viên, cần xóa cả tài khoản trong bảng users
                string sql = @"
                    DECLARE @userId INT;
                    SELECT @userId = user_id FROM teachers WHERE id = @Id;
                    DELETE FROM teachers WHERE id = @Id;
                    DELETE FROM users WHERE id = @userId;";
                return db.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; // Bắt buộc phải có để đọc chuỗi kết nối
using Model;
using DAL.Interfaces; // Đảm bảo bạn đã using thư mục chứa IUserRepository
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // 1. Phải là public và kế thừa IUserRepository
    public class UserRepository : IUserRepository
    {
        // 2. Khai báo biến chứa chuỗi kết nối
        private readonly string _connectionString;

        // 3. Constructor lấy chuỗi kết nối từ appsettings.json
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public LoggedInUser ValidateUser(string username, string password)
        {
            // 4. Sử dụng biến _connectionString đã được khởi tạo
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT id AS Id, username AS Username, role AS Role 
            FROM users 
            WHERE username = @Username 
              AND password_hash = CONVERT(VARCHAR(255), HASHBYTES('SHA2_256', 'Tt@' + @Password), 2)";

                return db.QueryFirstOrDefault<LoggedInUser>(sql, new { Username = username, Password = password });
            }
        }
    }
}
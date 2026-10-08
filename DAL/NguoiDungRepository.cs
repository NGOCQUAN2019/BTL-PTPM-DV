using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly string _connectionString;

        public NguoiDungRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public LoggedInUser ValidateUser(string username, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Đã sửa: Thêm CAST('Tt@' + @Password AS VARCHAR(255)) để đồng bộ Hash
                string sql = @"
            SELECT id AS Id, username AS Username, role AS Role 
            FROM users 
            WHERE username = @Username 
              AND password_hash = CONVERT(VARCHAR(255), HASHBYTES('SHA2_256', CAST('Tt@' + @Password AS VARCHAR(255))), 2)";

                return db.QueryFirstOrDefault<LoggedInUser>(sql, new { Username = username, Password = password });
            }
        }
    }
}
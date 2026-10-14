using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System.Data;

namespace DAL
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly string _connectionString;

        public NguoiDungRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Đổi tên tham số thành tenDangNhap, matKhau cho đồng bộ
        public LoggedInUser ValidateUser(string tenDangNhap, string matKhau)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT Id, TenDangNhap, VaiTro 
            FROM NguoiDung 
            WHERE TenDangNhap = @TenDangNhap 
              AND MatKhauHash = CONVERT(VARCHAR(255), HASHBYTES('SHA2_256', CAST('Tt@' + @MatKhau AS VARCHAR(255))), 2)";

                return db.QueryFirstOrDefault<LoggedInUser>(sql, new { TenDangNhap = tenDangNhap, MatKhau = matKhau });
            }
        }
    }
}
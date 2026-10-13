using DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Data;
using System.Collections.Generic;

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
                // Tên cột trong SQL giờ đã giống hệt tên Property trong Model nên không cần dùng AS nữa
                string sql = @"SELECT Id, NguoiDungId, MaGiaoVien, HoTen, SoDienThoai, GioiTinh 
                               FROM GiaoVien";
                return db.Query<GiaoVienModel>(sql);
            }
        }

        public GiaoVienModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT Id, NguoiDungId, MaGiaoVien, HoTen, SoDienThoai, GioiTinh 
                               FROM GiaoVien WHERE Id = @Id";
                return db.QueryFirstOrDefault<GiaoVienModel>(sql, new { Id = id });
            }
        }

        public bool Create(GiaoVienModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Lệnh Insert đơn giản, Trigger trg_GiaoVien_TaoTaiKhoan dưới SQL sẽ tự lo phần tạo User
                string sql = @"INSERT INTO GiaoVien (MaGiaoVien, HoTen, SoDienThoai, GioiTinh) 
                               VALUES (@MaGiaoVien, @HoTen, @SoDienThoai, @GioiTinh)";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Update(GiaoVienModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"UPDATE GiaoVien 
                               SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, GioiTinh = @GioiTinh 
                               WHERE Id = @Id";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Xóa giáo viên và xóa luôn tài khoản tương ứng trong bảng NguoiDung
                string sql = @"
                    DECLARE @userId INT;
                    SELECT @userId = NguoiDungId FROM GiaoVien WHERE Id = @Id;
                    DELETE FROM GiaoVien WHERE Id = @Id;
                    DELETE FROM NguoiDung WHERE Id = @userId;";
                return db.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}
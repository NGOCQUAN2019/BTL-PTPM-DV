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
    public class HocVienRepository : IHocVienRepository
    {
        private readonly string _connectionString;

        public HocVienRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CreateStudent(HocVienModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Khớp tham số với @p_MaHocVien, @p_HoTen... trong Procedure sp_ThemHocVien
                var parameters = new
                {
                    p_MaHocVien = model.MaHocVien,
                    p_HoTen = model.HoTen,
                    p_Email = model.Email,
                    p_GioiTinh = model.GioiTinh // Bổ sung giới tính
                };
                db.Execute("sp_ThemHocVien", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool UpdateStudent(HocVienModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    p_MaHocVien = model.MaHocVien,
                    p_HoTen = model.HoTen,
                    p_Email = model.Email
                };
                db.Execute("sp_CapNhatHocVien", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool DeleteStudent(string maHocVien)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Dùng lệnh SQL trực tiếp để xóa sạch cả Học Viên và Tài Khoản liên kết
                string sql = @"
                    DECLARE @userId INT;
                    SELECT @userId = NguoiDungId FROM HocVien WHERE MaHocVien = @MaHocVien;
                    DELETE FROM HocVien WHERE MaHocVien = @MaHocVien;
                    DELETE FROM NguoiDung WHERE Id = @userId;";

                db.Execute(sql, new { MaHocVien = maHocVien });
                return true;
            }
        }

        public HocVienModel GetStudentById(string maHocVien)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_MaHocVien = maHocVien };
                return db.QueryFirstOrDefault<HocVienModel>("sp_LayHocVienTheoMa", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<HocVienModel> SearchStudents(string tuKhoa)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { p_TuKhoa = tuKhoa ?? "" };
                return db.Query<HocVienModel>("sp_TimKiemHocVien", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
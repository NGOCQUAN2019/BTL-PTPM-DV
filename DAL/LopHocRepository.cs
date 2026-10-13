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
                // Tên cột đã giống hệt tên Property trong Model nên truy vấn rất ngắn gọn
                string sql = @"SELECT Id, MaLopHoc, KhoaHocId, GiaoVienId, PhongHoc, LichHoc, 
                                      GioBatDau, GioKetThuc, NgayBatDau, NgayKetThuc, 
                                      SiSoToiDa, TrangThai 
                               FROM LopHoc";
                return db.Query<LopHocModel>(sql);
            }
        }

        public LopHocModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT Id, MaLopHoc, KhoaHocId, GiaoVienId, PhongHoc, LichHoc, 
                                      GioBatDau, GioKetThuc, NgayBatDau, NgayKetThuc, 
                                      SiSoToiDa, TrangThai 
                               FROM LopHoc WHERE Id = @Id";
                return db.QueryFirstOrDefault<LopHocModel>(sql, new { Id = id });
            }
        }

        public bool Create(LopHocModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"INSERT INTO LopHoc (MaLopHoc, KhoaHocId, GiaoVienId, PhongHoc, LichHoc, GioBatDau, GioKetThuc, NgayBatDau, NgayKetThuc, SiSoToiDa, TrangThai) 
                               VALUES (@MaLopHoc, @KhoaHocId, @GiaoVienId, @PhongHoc, @LichHoc, @GioBatDau, @GioKetThuc, @NgayBatDau, @NgayKetThuc, @SiSoToiDa, @TrangThai)";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Update(LopHocModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"UPDATE LopHoc 
                               SET MaLopHoc = @MaLopHoc, KhoaHocId = @KhoaHocId, GiaoVienId = @GiaoVienId, 
                                   PhongHoc = @PhongHoc, LichHoc = @LichHoc, GioBatDau = @GioBatDau, 
                                   GioKetThuc = @GioKetThuc, NgayBatDau = @NgayBatDau, end_date = @NgayKetThuc, 
                                   SiSoToiDa = @SiSoToiDa, TrangThai = @TrangThai 
                               WHERE Id = @Id";
                // Lưu ý nhỏ: Chữ 'end_date' ở code cũ đã được tôi sửa thành 'NgayKetThuc' trong đoạn SQL trên cho đồng bộ.

                // Đảm bảo sửa triệt để chỗ end_date bị sót lại:
                sql = sql.Replace("end_date = @NgayKetThuc", "NgayKetThuc = @NgayKetThuc");

                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Thay 'classes' thành 'LopHoc'
                return db.Execute("DELETE FROM LopHoc WHERE Id = @Id", new { Id = id }) > 0;
            }
        }
    }
}
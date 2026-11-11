using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System;

namespace DAL
{
    public class GhiDanhRepository : IGhiDanhRepository
    {
        private readonly string _conn;

        public GhiDanhRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<GhiDanhModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Sửa lại đúng các cột của bảng GhiDanh
                string sql = "SELECT Id, HocVienId, LopHocId, NgayGhiDanh, TrangThai FROM GhiDanh";
                return db.Query<GhiDanhModel>(sql);
            }
        }

        public GhiDanhModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "SELECT Id, HocVienId, LopHocId, NgayGhiDanh, TrangThai FROM GhiDanh WHERE Id = @Id";
                return db.QueryFirstOrDefault<GhiDanhModel>(sql, new { Id = id });
            }
        }

        // --- HÀM MỚI BỔ SUNG CHO API_USER ---
        public IEnumerable<GhiDanhModel> GetByHocVienId(int hocVienId)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "SELECT Id, HocVienId, LopHocId, NgayGhiDanh, TrangThai FROM GhiDanh WHERE HocVienId = @HocVienId";
                return db.Query<GhiDanhModel>(sql, new { HocVienId = hocVienId });
            }
        }

        public bool Create(GhiDanhModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"
                    DECLARE @RealHocVienId INT = (SELECT Id FROM HocVien WHERE NguoiDungId = @IdTuToken);
            
                    IF @RealHocVienId IS NULL
                        THROW 50001, N'Không tìm thấy hồ sơ Học viên cho tài khoản này.', 1;
            
                    EXEC sp_GhiDanhHocVien @RealHocVienId, @LopHocId;
                ";

                var parameters = new DynamicParameters();
                parameters.Add("@IdTuToken", model.HocVienId);
                parameters.Add("@LopHocId", model.LopHocId);

                try
                {
                    db.Execute(sql, parameters);
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        throw new Exception("Học viên này đã ghi danh vào lớp học này rồi!");
                    }
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Update(GhiDanhModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Chỉ Update những trường có thật trong bảng GhiDanh
                string sql = @"UPDATE GhiDanh 
                               SET HocVienId = @HocVienId, 
                                   LopHocId = @LopHocId, 
                                   TrangThai = @TrangThai 
                               WHERE Id = @Id";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Execute("DELETE FROM GhiDanh WHERE Id = @Id", new { Id = id }) > 0;
            }
        }
    }
}
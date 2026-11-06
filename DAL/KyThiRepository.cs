using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class KyThiRepository : IKyThiRepository
    {
        private readonly string _conn;

        public KyThiRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<KyThiModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Query<KyThiModel>("SELECT * FROM KyThi ORDER BY NgayThi DESC");
            }
        }

        public KyThiModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.QueryFirstOrDefault<KyThiModel>("SELECT * FROM KyThi WHERE Id = @Id", new { Id = id });
            }
        }

        public bool Create(KyThiModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Chuỗi SQL đã được thay đổi, chỉ gọi đúng 7 cột có trong bảng SQL hiện tại
                string sql = @"
            INSERT INTO KyThi (LoaiKyThi, TenKyThi, KhoaHocId, LopHocId, NgayThi, DiemToiDa, HeSo)
            VALUES (@LoaiKyThi, @TenKyThi, @KhoaHocId, @LopHocId, @NgayThi, @DiemToiDa, @HeSo)";
                try
                {
                    return db.Execute(sql, model) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi thêm kỳ thi: " + ex.Message);
                }
            }
        }

        public bool Update(KyThiModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Tương tự, lệnh UPDATE cũng phải bỏ các biến cũ (PhongThi, ThoiGianLamBai...)
                string sql = @"
            UPDATE KyThi 
            SET LoaiKyThi = @LoaiKyThi, 
                TenKyThi = @TenKyThi, 
                KhoaHocId = @KhoaHocId, 
                LopHocId = @LopHocId, 
                NgayThi = @NgayThi, 
                DiemToiDa = @DiemToiDa, 
                HeSo = @HeSo
            WHERE Id = @Id";
                try
                {
                    return db.Execute(sql, model) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật kỳ thi: " + ex.Message);
                }
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "DELETE FROM KyThi WHERE Id = @Id";
                try
                {
                    return db.Execute(sql, new { Id = id }) > 0;
                }
                catch (SqlException ex)
                {
                    // Lỗi 547 là lỗi vi phạm khóa ngoại (Foreign Key)
                    if (ex.Number == 547)
                        throw new Exception("Không thể xóa kỳ thi này vì đã có dữ liệu điểm số của học viên.");

                    throw new Exception("Lỗi khi xóa kỳ thi: " + ex.Message);
                }
            }
        }
    }
}
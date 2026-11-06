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
    public class DiemSoRepository : IDiemSoRepository
    {
        private readonly string _conn;

        public DiemSoRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<DiemSoModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Query<DiemSoModel>("SELECT * FROM DiemSo");
            }
        }

        public DiemSoModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.QueryFirstOrDefault<DiemSoModel>("SELECT * FROM DiemSo WHERE Id = @Id", new { Id = id });
            }
        }

        public bool Create(DiemSoModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"
                    INSERT INTO DiemSo (KyThiId, HocVienId, Diem, KhoaHocDeXuatId, GhiChu)
                    VALUES (@KyThiId, @HocVienId, @Diem, @KhoaHocDeXuatId, @GhiChu)";
                try
                {
                    return db.Execute(sql, model) > 0;
                }
                catch (SqlException ex)
                {
                    // Bẫy lỗi Ràng buộc UNIQUE (KyThiId, HocVienId)
                    if (ex.Number == 2627)
                    {
                        throw new Exception("Học viên này đã có điểm trong kỳ thi này rồi!");
                    }
                    throw new Exception("Lỗi khi nhập điểm: " + ex.Message);
                }
            }
        }

        public bool Update(DiemSoModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Khi update điểm, không cho phép đổi người thi hay đổi môn thi, chỉ đổi kết quả
                string sql = @"
                    UPDATE DiemSo 
                    SET Diem = @Diem, 
                        KhoaHocDeXuatId = @KhoaHocDeXuatId, 
                        GhiChu = @GhiChu
                    WHERE Id = @Id";
                try
                {
                    return db.Execute(sql, model) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật điểm: " + ex.Message);
                }
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "DELETE FROM DiemSo WHERE Id = @Id";
                try
                {
                    return db.Execute(sql, new { Id = id }) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi xóa điểm: " + ex.Message);
                }
            }
        }
    }
}
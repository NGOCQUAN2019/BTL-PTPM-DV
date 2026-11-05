using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data;

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
                string sql = @"SELECT Id, MaHocVien, LopHocId, NgayGhiDanh, SoTienDaDong, TrangThai 
                               FROM GhiDanh";
                return db.Query<GhiDanhModel>(sql);
            }
        }

        public GhiDanhModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT Id, MaHocVien, LopHocId, NgayGhiDanh, SoTienDaDong, TrangThai 
                               FROM GhiDanh 
                               WHERE Id = @Id";
                return db.QueryFirstOrDefault<GhiDanhModel>(sql, new { Id = id });
            }
        }

        public bool Create(GhiDanhModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"
            DECLARE @hvId INT = (SELECT Id FROM HocVien WHERE MaHocVien = @MaHocVien);
            
            IF @hvId IS NULL
                THROW 50001, N'Không tìm thấy mã học viên này trong hệ thống.', 1;
            ELSE
                EXEC sp_GhiDanhHocVien @hvId, @LopHocId;";

                try
                {
                   
                    db.Execute(sql, model);
                    return true;
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
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
                string sql = @"UPDATE GhiDanh 
                               SET MaHocVien = @MaHocVien, LopHocId = @LopHocId, 
                                   NgayGhiDanh = @NgayGhiDanh, SoTienDaDong = @SoTienDaDong, 
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
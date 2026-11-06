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
    public class ChungChiRepository : IChungChiRepository
    {
        private readonly string _conn;

        public ChungChiRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ChungChiModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Query<ChungChiModel>("SELECT * FROM ChungChi ORDER BY NgayCap DESC");
            }
        }

        public ChungChiModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.QueryFirstOrDefault<ChungChiModel>("SELECT * FROM ChungChi WHERE Id = @Id", new { Id = id });
            }
        }

        public bool CapChungChi(int ghiDanhId)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                try
                {
                    // Chạy Stored Procedure bạn đã viết sẵn
                    db.Execute("sp_CapChungChi", new { p_GhiDanhId = ghiDanhId }, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        throw new Exception("Học viên này đã được cấp chứng chỉ rồi!");
                    }

                    throw new Exception(ex.Message);
                }
            }
        }

        public bool UpdatePdf(int id, string duongDanPdf)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "UPDATE ChungChi SET DuongDanPdf = @DuongDanPdf WHERE Id = @Id";
                return db.Execute(sql, new { Id = id, DuongDanPdf = duongDanPdf }) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Khi xóa chứng chỉ, ta rollback trạng thái GhiDanh về 'DangHoc'
                string sql = @"
                    DECLARE @gDanhId INT = (SELECT GhiDanhId FROM ChungChi WHERE Id = @Id);
                    DELETE FROM ChungChi WHERE Id = @Id;
                    IF @gDanhId IS NOT NULL
                        UPDATE GhiDanh SET TrangThai = 'DangHoc' WHERE Id = @gDanhId;";
                try
                {
                    return db.Execute(sql, new { Id = id }) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi xóa chứng chỉ: " + ex.Message);
                }
            }
        }
    }
}
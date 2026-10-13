using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class KhoaHocRepository : IKhoaHocRepository
    {
        private readonly string _conn;

        public KhoaHocRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<KhoaHocModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Tên cột đã khớp 100% với Model, không cần dùng AS
                string sql = @"SELECT Id, MaKhoaHoc, TenKhoaHoc, CapDo, 
                                      ThoiLuongGio, HocPhi, YeuCauThiDauVao, DiemDat 
                               FROM KhoaHoc";
                return db.Query<KhoaHocModel>(sql);
            }
        }

        public KhoaHocModel GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"SELECT Id, MaKhoaHoc, TenKhoaHoc, CapDo, 
                                      ThoiLuongGio, HocPhi, YeuCauThiDauVao, DiemDat 
                               FROM KhoaHoc WHERE Id = @Id";
                return db.QueryFirstOrDefault<KhoaHocModel>(sql, new { Id = id });
            }
        }

        public bool Create(KhoaHocModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"INSERT INTO KhoaHoc (MaKhoaHoc, TenKhoaHoc, CapDo, ThoiLuongGio, HocPhi, YeuCauThiDauVao, DiemDat) 
                               VALUES (@MaKhoaHoc, @TenKhoaHoc, @CapDo, @ThoiLuongGio, @HocPhi, @YeuCauThiDauVao, @DiemDat)";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Update(KhoaHocModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = @"UPDATE KhoaHoc 
                               SET MaKhoaHoc = @MaKhoaHoc, TenKhoaHoc = @TenKhoaHoc, CapDo = @CapDo, 
                                   ThoiLuongGio = @ThoiLuongGio, HocPhi = @HocPhi, 
                                   YeuCauThiDauVao = @YeuCauThiDauVao, DiemDat = @DiemDat 
                               WHERE Id = @Id";
                return db.Execute(sql, model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Thay 'courses' thành 'KhoaHoc'
                return db.Execute("DELETE FROM KhoaHoc WHERE Id = @Id", new { Id = id }) > 0;
            }
        }
    }
}
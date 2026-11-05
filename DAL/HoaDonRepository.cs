using DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly string _conn;

        public HoaDonRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<HoaDonModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Query<HoaDonModel>("SELECT * FROM HoaDon ORDER BY HanThanhToan DESC");
            }
        }

        public IEnumerable<HoaDonModel> GetByHocVien(int hocVienId)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Query<HoaDonModel>("SELECT * FROM HoaDon WHERE HocVienId = @Id ORDER BY HanThanhToan DESC", new { Id = hocVienId });
            }
        }
    }
}
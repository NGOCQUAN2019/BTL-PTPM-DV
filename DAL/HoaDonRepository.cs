using DAL.Interfaces;
using Model;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly string _conn;
        public HoaDonRepository(IConfiguration configuration) => _conn = configuration.GetConnectionString("DefaultConnection");

        public IEnumerable<HoaDonModel> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                return db.Query<HoaDonModel>("SELECT * FROM HoaDon ORDER BY HanThanhToan DESC");
            }
        }

        public IEnumerable<HoaDonModel> GetByHocVienId(int hocVienId)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Lọc hóa đơn để Học viên có thể xem mình đang nợ những khoản nào
                return db.Query<HoaDonModel>("SELECT * FROM HoaDon WHERE HocVienId = @HocVienId", new { HocVienId = hocVienId });
            }
        }
    }
}
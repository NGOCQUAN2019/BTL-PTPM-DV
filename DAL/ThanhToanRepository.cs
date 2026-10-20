using DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Data;

namespace DAL
{
    public class ThanhToanRepository : IThanhToanRepository
    {
        private readonly string _conn;
        public ThanhToanRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        // Gọi Stored Procedure sp_ThemThanhToan
        public bool ThemThanhToan(ThanhToanRequest request)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_HoaDonId", request.HoaDonId);
                parameters.Add("@p_SoTien", request.SoTien);
                parameters.Add("@p_PhuongThuc", request.PhuongThuc);

                // Dapper sẽ gọi SP, nếu số tiền đủ nó tự động đổi trạng thái Hóa Đơn và Ghi Danh
                int rowsAffected = db.Execute("sp_ThemThanhToan", parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }
        }

        // Lấy danh sách hóa đơn theo ID Học viên
        public IEnumerable<HoaDonModel> GetHoaDonByHocVien(int hocVienId)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "SELECT * FROM HoaDon WHERE HocVienId = @HocVienId ORDER BY HanThanhToan DESC";
                return db.Query<HoaDonModel>(sql, new { HocVienId = hocVienId });
            }
        }

        // Lấy chi tiết các lần nộp tiền của 1 hóa đơn
        public IEnumerable<ThanhToanModel> GetLichSuThanhToan(int hoaDonId)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                string sql = "SELECT * FROM ThanhToan WHERE HoaDonId = @HoaDonId ORDER BY NgayThanhToan DESC";
                return db.Query<ThanhToanModel>(sql, new { HoaDonId = hoaDonId });
            }
        }
    }
}
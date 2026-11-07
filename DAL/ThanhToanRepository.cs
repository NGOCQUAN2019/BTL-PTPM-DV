using DAL.Interfaces;
using Model;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ThanhToanRepository : IThanhToanRepository
    {
        private readonly string _conn;
        public ThanhToanRepository(IConfiguration configuration) => _conn = configuration.GetConnectionString("DefaultConnection");

        public bool Create(ThanhToanModel model)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                try
                {
                    // Truyền đúng 3 tham số mà SP sp_ThemThanhToan yêu cầu
                    var parameters = new
                    {
                        p_HoaDonId = model.HoaDonId,
                        p_SoTien = model.SoTien,
                        p_PhuongThuc = model.PhuongThuc
                    };

                    db.Execute("sp_ThemThanhToan", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex)
                {
                    // Lỗi 547 là lỗi khóa ngoại (VD: Nhập sai mã Hóa Đơn)
                    throw new Exception("Lỗi xử lý thanh toán: " + ex.Message);
                }
            }
        }
    }
}
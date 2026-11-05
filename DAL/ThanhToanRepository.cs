using DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Data;
using System;

namespace DAL
{
    public class ThanhToanRepository : IThanhToanRepository
    {
        private readonly string _conn;

        public ThanhToanRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public bool Create(ThanhToanRequest request)
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_HoaDonId", request.HoaDonId);
                parameters.Add("@p_SoTien", request.SoTien);
                parameters.Add("@p_PhuongThuc", request.PhuongThuc);

                try
                {
                    return db.Execute("sp_ThemThanhToan", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi thanh toán: " + ex.Message);
                }
            }
        }
    }
}
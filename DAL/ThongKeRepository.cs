using DAL.Interfaces;
using Model;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly string _conn;

        public ThongKeRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ThongKeLopHocModel> GetThongKeLopHoc()
        {
            using (IDbConnection db = new SqlConnection(_conn))
            {
                // Gọi thẳng View đã được bạn tính toán (SUM, COUNT, AVG) sẵn dưới CSDL
                string sql = "SELECT * FROM v_ThongKeLopHoc ORDER BY TrangThai, MaLopHoc";
                return db.Query<ThongKeLopHocModel>(sql);
            }
        }
    }
}
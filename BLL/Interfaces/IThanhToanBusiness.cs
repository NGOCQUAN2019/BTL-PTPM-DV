using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IThanhToanBusiness
    {
        bool ThemThanhToan(ThanhToanRequest request);
        IEnumerable<HoaDonModel> GetHoaDonByHocVien(int hocVienId);
        IEnumerable<ThanhToanModel> GetLichSuThanhToan(int hoaDonId);
    }
}
using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IThanhToanRepository
    {
        // Gọi Stored Procedure thêm thanh toán
        bool ThemThanhToan(ThanhToanRequest request);

        // Truy vấn danh sách hóa đơn của 1 học viên
        IEnumerable<HoaDonModel> GetHoaDonByHocVien(int hocVienId);

        // Truy vấn lịch sử các lần nộp tiền của 1 hóa đơn
        IEnumerable<ThanhToanModel> GetLichSuThanhToan(int hoaDonId);
    }
}
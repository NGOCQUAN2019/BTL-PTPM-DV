using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class ThanhToanBusiness : IThanhToanBusiness
    {
        private readonly IThanhToanRepository _thanhToanRepository;

        // Tiêm IThanhToanRepository thông qua Constructor (Dependency Injection)
        public ThanhToanBusiness(IThanhToanRepository thanhToanRepository)
        {
            _thanhToanRepository = thanhToanRepository;
        }

        public bool ThemThanhToan(ThanhToanRequest request)
        {
            // Nếu cần, bạn có thể kiểm tra logic trước khi lưu
            // Ví dụ: Kiểm tra xem phương thức thanh toán có hợp lệ không
            if (request.PhuongThuc != "TienMat" && request.PhuongThuc != "ChuyenKhoan" && request.PhuongThuc != "The")
            {
                throw new System.Exception("Phương thức thanh toán không hợp lệ!");
            }

            return _thanhToanRepository.ThemThanhToan(request);
        }

        public IEnumerable<HoaDonModel> GetHoaDonByHocVien(int hocVienId)
        {
            return _thanhToanRepository.GetHoaDonByHocVien(hocVienId);
        }

        public IEnumerable<ThanhToanModel> GetLichSuThanhToan(int hoaDonId)
        {
            return _thanhToanRepository.GetLichSuThanhToan(hoaDonId);
        }
    }
}
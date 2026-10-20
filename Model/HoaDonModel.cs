using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HoaDonModel
    {
        public int Id { get; set; }
        public string MaHoaDon { get; set; }
        public int GhiDanhId { get; set; }
        public int HocVienId { get; set; }
        public decimal TongTien { get; set; }
        public DateTime HanThanhToan { get; set; }
        public string TrangThai { get; set; }
    }

    // Dùng cho API Admin khi nộp tiền
    public class ThanhToanRequest
    {
        public int HoaDonId { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; } // 'TienMat', 'ChuyenKhoan', 'The'
    }
}
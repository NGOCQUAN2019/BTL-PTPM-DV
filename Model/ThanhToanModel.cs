using System;

namespace Model
{
    public class ThanhToanModel
    {
        public int Id { get; set; }
        public int HoaDonId { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; } // TienMat, ChuyenKhoan, The
        public DateTime NgayThanhToan { get; set; }
    }
}
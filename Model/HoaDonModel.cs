using System;

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
        public string TrangThai { get; set; } // ChuaThanhToan, ThanhToanMotPhan, DaThanhToan
    }
}
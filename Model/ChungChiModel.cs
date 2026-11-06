using System;

namespace Model
{
    public class ChungChiModel
    {
        public int Id { get; set; }
        public string MaChungChi { get; set; }
        public Guid MaXacThuc { get; set; } 
        public int GhiDanhId { get; set; }
        public int HocVienId { get; set; }
        public int KhoaHocId { get; set; }
        public decimal? DiemTongKet { get; set; }
        public DateTime NgayCap { get; set; }
        public string DuongDanPdf { get; set; }
    }
}
using System;

namespace Model
{
    public class GhiDanhModel
    {
        public int Id { get; set; }
        public string MaHocVien { get; set; }
        public int LopHocId { get; set; }
        public DateTime NgayGhiDanh { get; set; }
        public decimal SoTienDaDong { get; set; }
        public string TrangThai { get; set; }
    }
}
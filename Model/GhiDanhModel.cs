using System;

namespace Model
{
    public class GhiDanhModel
    {
        public int Id { get; set; }
        public string MaHocVien { get; set; } // Liên kết với bảng HocVien
        public int LopHocId { get; set; }     // Liên kết với bảng LopHoc
        public DateTime NgayGhiDanh { get; set; }
        public decimal SoTienDaDong { get; set; }
        public string TrangThai { get; set; } // Ví dụ: "Chờ duyệt", "Đã duyệt", "Đã hủy"
    }
}
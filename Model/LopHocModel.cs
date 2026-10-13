using System;

namespace Model
{
    public class LopHocModel
    {
        public int Id { get; set; }
        public string MaLopHoc { get; set; }
        public int KhoaHocId { get; set; }
        public int GiaoVienId { get; set; }
        public string PhongHoc { get; set; }
        public string LichHoc { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int SiSoToiDa { get; set; } = 20;
        public string TrangThai { get; set; } = "MoDangKy";
    }
}
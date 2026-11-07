using System;

namespace Model
{
    public class ThongKeLopHocModel
    {
        public string MaLopHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public string TenGiaoVien { get; set; }
        public string TrangThai { get; set; }
        public int SiSoToiDa { get; set; }
        public int DaGhiDanh { get; set; }

        public decimal? DiemTrungBinhLop { get; set; }
        public decimal? TyLeDau { get; set; }

        public decimal TongNoHocPhi { get; set; }
    }
}
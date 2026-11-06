using System;

namespace Model
{
    public class KyThiModel
    {
        public int Id { get; set; }

        public string LoaiKyThi { get; set; }

        public string TenKyThi { get; set; }

        public int? KhoaHocId { get; set; }
        public int? LopHocId { get; set; }

        public DateTime NgayThi { get; set; }

        public decimal DiemToiDa { get; set; }
        public decimal HeSo { get; set; }
    }
}
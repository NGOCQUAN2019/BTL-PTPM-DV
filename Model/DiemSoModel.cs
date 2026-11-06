using System;

namespace Model
{
    public class DiemSoModel
    {
        public int Id { get; set; }
        public int KyThiId { get; set; }
        public int HocVienId { get; set; }
        public decimal Diem { get; set; }

        // Cho phép NULL vì khóa học đề xuất chỉ dùng sau khi thi cuối kỳ hoặc xếp lớp
        public int? KhoaHocDeXuatId { get; set; }
        public string GhiChu { get; set; }
    }
}
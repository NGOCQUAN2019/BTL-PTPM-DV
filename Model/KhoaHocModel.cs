using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class KhoaHocModel
    {
        public int Id { get; set; }
        public string MaKhoaHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public string CapDo { get; set; }
        public int ThoiLuongGio { get; set; }
        public decimal HocPhi { get; set; }
        public bool YeuCauThiDauVao { get; set; }
        public decimal DiemDat { get; set; }
    }
}
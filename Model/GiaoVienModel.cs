using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GiaoVienModel
    {
        public int Id { get; set; }
        public int? NguoiDungId { get; set; }
        public string MaGiaoVien { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string GioiTinh { get; set; }
    }
}
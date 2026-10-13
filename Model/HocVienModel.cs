using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HocVienModel
    {
        public int Id { get; set; }
        public int? NguoiDungId { get; set; }
        public string MaHocVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ThanhToanModel
    {
        public int Id { get; set; }
        public int HoaDonId { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; }
        public DateTime NgayThanhToan { get; set; }
    }
}
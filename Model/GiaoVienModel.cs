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
        public int? UserId { get; set; }
        public string TeacherCode { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
    }
}

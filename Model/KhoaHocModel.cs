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
        public string Code { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public int DurationHours { get; set; }
        public decimal TuitionFee { get; set; }
        public bool RequiresPlacement { get; set; }
        public decimal PassScore { get; set; }
        public decimal MinAttendancePct { get; set; }
    }
}

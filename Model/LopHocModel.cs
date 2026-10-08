using System;

namespace Model
{
    public class LopHocModel
    {
        public int Id { get; set; }
        public string ClassCode { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Room { get; set; }
        public string Weekdays { get; set; }
        public TimeSpan? StartTime { get; set; } // SQL là TIME nên dùng TimeSpan
        public TimeSpan? EndTime { get; set; }
        public DateTime StartDate { get; set; }  // SQL là DATE nên dùng DateTime
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; } = 20;
        public string Status { get; set; } = "open"; // Ràng buộc SQL: open, running, finished, cancelled
    }
}
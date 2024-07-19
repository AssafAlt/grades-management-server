using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class AttendanceReportDto
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<DateTime, string> AttendanceDates { get; set; } = new Dictionary<DateTime, string>();
        public int TotalAttendances { get; set; }
    }
}
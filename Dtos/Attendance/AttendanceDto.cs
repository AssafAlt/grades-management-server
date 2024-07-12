using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class AttendanceDto
    {
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
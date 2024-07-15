using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class AttendanceDto
    {
        public string Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
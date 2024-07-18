using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class CreateAttendancesReportDto
    {
        [Required]
        public List<CreateAttendanceDto> AttendanceDtos { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }

}
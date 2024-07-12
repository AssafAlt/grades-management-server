using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Dtos.Grade;

namespace api.Dtos.Student
{
    public class StudentDto
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<AttendanceDto> Attendances { get; set; }
        public ICollection<GradeDto> Grades { get; set; }
    }
}
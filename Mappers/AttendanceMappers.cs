using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Models;

namespace api.Mappers
{
    public static class AttendanceMappers
    {
        public static Attendance ToAttendanceFromCreate(this CreateAttendanceDto attendanceDto)
        {
            return new Attendance
            {
                StudentId = attendanceDto.StudentId,
                ClassId = attendanceDto.ClassId,
                Date = attendanceDto.Date.Date,
                IsPresent = attendanceDto.IsPresent

            };
        }

    }
}


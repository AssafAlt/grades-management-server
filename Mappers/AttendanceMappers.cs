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
        public static List<Attendance> ToAttendancesFromCreate(this CreateAttendancesReportDto attendancesReportDto, int classId)
        {
            return attendancesReportDto.AttendanceDtos.Select(a => new Attendance
            {
                StudentId = a.StudentId,
                ClassId = classId,
                Date = attendancesReportDto.Date,
                IsPresent = a.IsPresent
            }).ToList();

        }

    }
}


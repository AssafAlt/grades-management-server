using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IAttendanceRepository
    {
        public Task CreateReportAsync(List<Attendance> attendances);
    }
}
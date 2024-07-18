using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces.Repository;
using api.Models;

namespace api.repository
{
    public class AttendanceRepository : IAttendanceRepository
    {

        private readonly ApplicationDBContext _context;

        public AttendanceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task CreateReportAsync(List<Attendance> attendances)
        {
            await _context.Attendances.AddRangeAsync(attendances);
            await _context.SaveChangesAsync();
        }
    }
}
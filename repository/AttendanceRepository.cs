using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces.Repository;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Attendance>> GetAttendancesByClassIdAsync(int classId)
        {
            return await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.ClassId == classId)
                .ToListAsync();
        }

    }
}
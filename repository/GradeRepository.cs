using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces.Repository;
using api.Models;

namespace api.repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDBContext _context;

        public GradeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Grade grade)
        {

            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();

        }
    }
}
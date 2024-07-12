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
    public class StudentRepository : IStudentRepository
    {

        private readonly ApplicationDBContext _context;

        public StudentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Student studentModel)
        {
            await _context.Students.AddAsync(studentModel);
            await _context.SaveChangesAsync();
            
        }

        public async Task<int?> DeleteAsync(string studentId)
        {
             var studentModel = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (studentModel == null) return null;
                
            
            _context.Students.Remove(studentModel);
            await _context.SaveChangesAsync();

            return 1;
            
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var students = _context.Students.Include(s => s.Classes);

            if(students == null) return null;
            return await students.ToListAsync();
        }

      
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Student;
using api.Interfaces.Repository;
using api.Mappers;
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

        public async Task<NewStudentDto> CreateAsync(Student studentModel)
        {
            await _context.Students.AddAsync(studentModel);
            await _context.SaveChangesAsync();
            return studentModel.ToNewStudentDtoFromModel();

        }

        public async Task<NewStudentDto[]> CreateMultipleAsync(Student[] studentModels)
        {
            // Get the IDs of students that already exist in the database
            var existingStudentIds = await _context.Students
                                                   .Where(s => studentModels.Select(sm => sm.StudentId).Contains(s.StudentId))
                                                   .Select(s => s.StudentId)
                                                   .ToListAsync();

            // Filter out students that already exist
            var newStudents = studentModels.Where(sm => !existingStudentIds.Contains(sm.StudentId)).ToArray();

            if (newStudents.Any())
            {
                await _context.Students.AddRangeAsync(newStudents);
                await _context.SaveChangesAsync();

            }
            var newStudentsDto = newStudents.Select(s => s.ToNewStudentDtoFromModel()).ToArray();

            // Return the newly created students
            return newStudentsDto;
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
            return await _context.Students.ToListAsync();
        }


    }
}
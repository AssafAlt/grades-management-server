using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Attendance;
using api.Dtos.Class;
using api.Dtos.Grade;
using api.Dtos.GradeItem;
using api.Dtos.Student;
using api.Interfaces.Repository;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDBContext _context;

        public ClassRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddStudentToClassAsync(int classId, string studentId)
        {
            var classModel = await _context.Classes
               .Include(c => c.Students)
               .FirstOrDefaultAsync(c => c.ClassId == classId);

            var student = await _context.Students.FindAsync(studentId);

            if (classModel == null || student == null) return;

            if (!classModel.Students.Any(s => s.StudentId == studentId))
            {
                classModel.Students.Add(student);

                await _context.SaveChangesAsync();


            }
        }
        public async Task RemoveStudentFromClassAsync(int classId, string studentId)
        {
            var classModel = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            var student = await _context.Students.FindAsync(studentId);

            if (classModel == null || student == null) return;

            if (classModel.Students.Any(s => s.StudentId == studentId))
            {
                classModel.Students.Remove(student);

                await _context.SaveChangesAsync();
            }
        }


        public async Task AddStudentsToClassAsync(int classId, List<string> studentIds)
        {
            var classModel = await _context.Classes
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.ClassId == classId);

            if (classModel == null) return;

            var studentsToAdd = await _context.Students
            .Where(s => studentIds.Contains(s.StudentId))
            .ToListAsync();

            foreach (var student in studentsToAdd)
            {
                if (!classModel.Students.Any(s => s.StudentId == student.StudentId)) classModel.Students.Add(student);
            }

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Class classModel)
        {
            await _context.Classes.AddAsync(classModel);
            await _context.SaveChangesAsync();

        }

        public async Task<List<TeacherClassesDto>> GetClassesByTeacherIdAsync(string teacherId)
        {
            var classDtos = await _context.Users
    .Where(u => u.Id == teacherId)
    .SelectMany(u => u.Classes)
    .Select(c => new TeacherClassesDto
    {
        ClassId = c.ClassId,
        ClassName = c.ClassName,
        Students = c.Students.Select(s => new StudentDto
        {
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            PhoneNumber = s.PhoneNumber,
            Attendances = s.Attendances.Select(a => new AttendanceDto
            {
                Date = a.Date,
                IsPresent = a.IsPresent
            }).ToList(),
            Grades = s.Grades.Select(g => new GradeDto
            {
                Name = g.GradeItem.Name,
                Weight = g.GradeItem.Weight,
                Score = g.Score
            }).ToList()
        }).ToList(),
        GradeItems = c.GradeItems.Select(g => new GradeItemDto
        {
            GradeItemId = g.GradeItemId,
            Name = g.Name,
            Weight = g.Weight
        }).ToList()
    }).ToListAsync();
            if (classDtos == null) return null;

            return classDtos;


        }

        public async Task<List<Student>> GetStudentsByClassIdAsync(int classId)
        {

            var studentsInClass = _context.Students
                .Where(s => s.Classes.Any(c => c.ClassId == classId));


            // Check if there are any students in the class
            if (studentsInClass == null) return null;


            return await studentsInClass.ToListAsync();
        }
    }
}
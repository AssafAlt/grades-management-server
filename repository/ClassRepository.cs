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


        public async Task<List<NewStudentDto>> AddStudentsToClassAsync(int classId, List<string> studentIds)
        {
            var classModel = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            if (classModel == null) return null;

            var studentsToAdd = await _context.Students
                .Where(s => studentIds.Contains(s.StudentId))
                .ToListAsync();

            var addedStudents = new List<NewStudentDto>();

            foreach (var student in studentsToAdd)
            {
                if (!classModel.Students.Any(s => s.StudentId == student.StudentId))
                {
                    classModel.Students.Add(student);
                    addedStudents.Add(student.ToNewStudentDtoFromModel());
                }
            }

            await _context.SaveChangesAsync();

            return addedStudents;
        }


        public async Task<NewClassDto> CreateAsync(Class classModel)
        {
            await _context.Classes.AddAsync(classModel);
            await _context.SaveChangesAsync();
            return classModel.ToNewClassDtoFromModel(0);

        }

        public async Task<List<NewClassDto>> GetClassesByTeacherIdAsync(string teacherId)
        {
            var classes = await _context.Users
            .Where(u => u.Id == teacherId)
            .SelectMany(u => u.Classes)
            .Include(c => c.Students)
            .Include(c => c.GradeItems)
            .ToListAsync();
            var classDtos = classes.Select(c => c.ToNewClassDtoFromModel(c.Students?.Count() ?? 0)).ToList();

            return classDtos;
        }
        public async Task<List<NewStudentDto>> GetStudentsByClassIdAsync(int classId)
        {
            var studentsInClass = await _context.Students
                .Where(s => s.Classes.Any(c => c.ClassId == classId))
                .Select(s => s.ToNewStudentDtoFromModel())
                .ToListAsync();

            // Check if there are any students in the class
            if (studentsInClass == null || !studentsInClass.Any()) return null;

            return studentsInClass;
        }

        Task<List<Student>> IClassRepository.GetStudentsByClassIdAsync(int classId)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> DeleteAsync(int classId)
        {
            var classModel = await _context.Classes.FirstOrDefaultAsync(x => x.ClassId == classId);

            if (classModel == null) return null;


            _context.Classes.Remove(classModel);
            await _context.SaveChangesAsync();

            return 1;
        }

        /* public async Task RemoveGradeItemFromClassAsync(int classId, int gradeItemId)
         {
             var classModel = await _context.Classes
                 .Include(c => c.GradeItems)
                 .FirstOrDefaultAsync(c => c.ClassId == classId);

             var gradeItem = await _context.GradeItems.FindAsync(gradeItemId);

             if (classModel == null || gradeItem == null) return;

             if (classModel.GradeItems.Any(s => s.GradeItemId == gradeItemId))
             {
                 classModel.GradeItems.Remove(gradeItem);

                 await _context.SaveChangesAsync();
             }
         }*/

    }
}

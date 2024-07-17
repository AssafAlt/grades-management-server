using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Class;
using api.Dtos.Student;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IClassRepository
    {
        public Task AddStudentToClassAsync(int classId, string studentId);
        public Task RemoveStudentFromClassAsync(int classId, string studentId);
        public Task<List<NewStudentDto>> AddStudentsToClassAsync(int classId, List<string> studentIds);
        public Task<NewClassDto> CreateAsync(Class classModel);
        //public Task<List<TeacherClassesDto>> GetClassesByTeacherIdAsync(string teacherId);
        public Task<List<NewClassDto>> GetClassesByTeacherIdAsync(string teacherId);
        public Task<List<Student>> GetStudentsByClassIdAsync(int classId);
    }
}
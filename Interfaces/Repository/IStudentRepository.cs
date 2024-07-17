using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Student;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IStudentRepository
    {
        public Task<NewStudentDto> CreateAsync(Student studentModel);
        public Task<NewStudentDto[]> CreateMultipleAsync(Student[] studentModels);
        public Task<int?> DeleteAsync(string studentId);
        public Task<List<Student>> GetAllAsync();
    }
}
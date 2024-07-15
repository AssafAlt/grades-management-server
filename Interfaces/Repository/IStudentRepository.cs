using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IStudentRepository
    {
        public Task CreateAsync(Student studentModel);
        public Task CreateMultipleAsync(Student[] studentModels);
        public Task<int?> DeleteAsync(string studentId);
        public Task<List<Student>> GetAllAsync();
    }
}
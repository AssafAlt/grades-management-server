using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Dtos.Grade;
using api.Dtos.Student;
using api.Models;

namespace api.Interfaces.Services
{
    public interface IStudentService
    {
        public Task<ServiceResult> CreateAsync(CreateStudentDto studentDto);
        public Task<ServiceResult> GetAllAsync();
        public Task<ServiceResult> DeleteAsync(string studentId);
        public Task<ServiceResult> CreateGradeAsync(CreateGradeDto gradeDto);
    }
}
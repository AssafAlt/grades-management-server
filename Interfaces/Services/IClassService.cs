using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Dtos.Attendance;
using api.Dtos.Class;
using api.Dtos.GradeItem;
using api.Models;

namespace api.Interfaces.Services
{
    public interface IClassService
    {
        public Task<ServiceResult> CreateAsync(CreateClassDto classDto, string teacherId);
        public Task<ServiceResult> AddStudentToClassAsync(int classId, string studentId);
        public Task<ServiceResult> RemoveStudentFromClassAsync(int classId, string studentId);
        public Task<ServiceResult> AddStudentsToClassAsync(int classId, List<string> studentIds);
        public Task<ServiceResult> GetStudentsByClassIdAsync(int classId);
        public Task<ServiceResult> GetAllClassesByTeacherId(string teacherId);
        public Task<ServiceResult> CreateAttendanceAsync(CreateAttendanceDto attendanceDto);
        public Task<ServiceResult> CreateGradeItemAsync(CreateGradeItemDto gradeItemDto);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Dtos.Attendance;
using api.Dtos.Class;
using api.Dtos.GradeItem;
using api.Interfaces;
using api.Interfaces.Repository;
using api.Interfaces.Services;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;


namespace api.Services
{
    public class ClassService : IClassService
    {

        private readonly IClassRepository _classRepo;
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly IGradeItemRepository _gradeItemRepo;


        public ClassService(IClassRepository classRepo, IAttendanceRepository attendanceRepo, IGradeItemRepository gradeItemRepo)
        {
            _classRepo = classRepo;
            _attendanceRepo = attendanceRepo;
            _gradeItemRepo = gradeItemRepo;

        }

        public async Task<ServiceResult> CreateAsync(CreateClassDto classDto, string teacherId)
        {
            try
            {

                var classModel = new Class
                {
                    ClassName = classDto.ClassName,
                    TeacherId = teacherId,
                    GroupId = classDto.GroupId

                };
                await _classRepo.CreateAsync(classModel);
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Class was created successfully" };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }

        }





        public async Task<ServiceResult> AddStudentToClassAsync(int classId, string studentId)
        {
            try
            {
                await _classRepo.AddStudentToClassAsync(classId, studentId);
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Message = "Student was added to the class!" };


            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
        public async Task<ServiceResult> RemoveStudentFromClassAsync(int classId, string studentId)
        {
            try
            {
                await _classRepo.RemoveStudentFromClassAsync(classId, studentId);
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Message = "Student was removed from the class!" };
            }
            catch (Exception ex)
            {
                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
        public async Task<ServiceResult> AddStudentsToClassAsync(int classId, List<string> studentIds)
        {
            try
            {
                await _classRepo.AddStudentsToClassAsync(classId, studentIds);
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Message = "Students were added to the class!" };
            }
            catch (Exception ex)
            {
                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult> GetStudentsByClassIdAsync(int classId)
        {

            try
            {
                var students = await _classRepo.GetStudentsByClassIdAsync(classId);
                if (students == null) return new ServiceResult { StatusCode = StatusCodes.Status404NotFound, Message = "There are no students in the class or Incorrect classId" };
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Data = students };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }

        }


        public async Task<ServiceResult> GetAllClassesByTeacherId(string teacherId)
        {
            try
            {
                var classes = await _classRepo.GetClassesByTeacherIdAsync(teacherId);
                if (classes == null) return new ServiceResult { StatusCode = StatusCodes.Status404NotFound, Message = "There are no classes" };
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Data = classes };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }


        public async Task<ServiceResult> CreateAttendanceAsync(CreateAttendanceDto attendanceDto)
        {
            try
            {
                await _attendanceRepo.CreateAsync(attendanceDto.ToAttendanceFromCreate());
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Attendance was created successfully" };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult> CreateGradeItemAsync(CreateGradeItemDto gradeItemDto)
        {
            try
            {
                await _gradeItemRepo.CreateAsync(gradeItemDto.ToGradeItemFromCreate());
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "GradeItem was created successfully" };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
    }
}



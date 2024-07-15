using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Dtos.Grade;
using api.Dtos.Student;
using api.Interfaces.Repository;
using api.Interfaces.Services;
using api.Mappers;
using api.Models;

namespace api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGradeRepository _gradeRepo;

        public StudentService(IStudentRepository studentRepo, IGradeRepository gradeRepo)
        {
            _studentRepo = studentRepo;
            _gradeRepo = gradeRepo;

        }
        public async Task<ServiceResult> CreateAsync(CreateStudentDto studentDto)
        {
            try
            {
                var studentModel = studentDto.ToStudentFromCreate();
                await _studentRepo.CreateAsync(studentModel);
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Student was created successfully" };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
        public async Task<ServiceResult> CreateGradeAsync(CreateGradeDto gradeDto)
        {
            try
            {
                await _gradeRepo.CreateAsync(gradeDto.ToGradeFromCreate());
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Student was created successfully" };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
        public async Task<ServiceResult> GetAllAsync()
        {
            try
            {
                var students = await _studentRepo.GetAllAsync();
                if (students == null) return new ServiceResult { StatusCode = StatusCodes.Status404NotFound, Message = "There are no students!" };
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Data = students };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
        public async Task<ServiceResult> DeleteAsync(string studentId)
        {
            try
            {
                var result = await _studentRepo.DeleteAsync(studentId);
                if (result == null) return new ServiceResult { StatusCode = StatusCodes.Status404NotFound, Message = "There are no student!" };
                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Message = "Student was deleted successfully!" };
            }
            catch (Exception ex)
            {

                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult> CreateMultipleAsync(CreateStudentDto[] studentDtos)
        {
            try
            {
                var studentModels = studentDtos.Select(dto => dto.ToStudentFromCreate()).ToArray();
                await _studentRepo.CreateMultipleAsync(studentModels);
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Students were created successfully" };
            }
            catch (Exception ex)
            {
                return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
            }
        }
    }
}

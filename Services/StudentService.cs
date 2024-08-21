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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGradeRepository _gradeRepo;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepo, IGradeRepository gradeRepo, ILogger<StudentService> logger)
        {
            _studentRepo = studentRepo;
            _gradeRepo = gradeRepo;
            _logger = logger;

        }
        public async Task<ServiceResult> CreateAsync(CreateStudentDto studentDto)
        {
            try
            {
                var studentModel = studentDto.ToStudentFromCreate();
                var newStudentDto = await _studentRepo.CreateAsync(studentModel);
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Student was created successfully", Data = newStudentDto };
            }
            catch (DbUpdateException dbEx)
            {

                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";

                return new ServiceResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbErrorMessage,
                    Data = null
                };
            }
        }
        public async Task<ServiceResult> CreateGradeAsync(CreateGradeDto gradeDto, string studentId)
        {
            try
            {
                await _gradeRepo.CreateAsync(gradeDto.ToGradeFromCreate(studentId));
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Student was created successfully" };
            }
            catch (DbUpdateException dbEx)
            {

                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";

                return new ServiceResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbErrorMessage,
                    Data = null
                };
            }
        }
        public async Task<ServiceResult> GetAllAsync()
        {
            try
            {
                var students = await _studentRepo.GetAllAsync();

                if (students == null || !students.Any())
                {
                    return new ServiceResult { StatusCode = StatusCodes.Status404NotFound, Message = "There are no students!" };
                }

                var studentsDto = students.Select(s => s.ToNewStudentDtoFromModel());

                return new ServiceResult { StatusCode = StatusCodes.Status200OK, Data = studentsDto };
            }
            catch (DbUpdateException dbEx)
            {

                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";

                return new ServiceResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbErrorMessage,
                    Data = null
                };
            }
        }
        public async Task<ServiceResult> DeleteAsync(string studentId)
        {
            try
            {
                var result = await _studentRepo.DeleteAsync(studentId);
                if (result == null) return new ServiceResult { StatusCode = StatusCodes.Status404NotFound, Message = "There are no student!" };
                return new ServiceResult { StatusCode = StatusCodes.Status204NoContent, Message = "Student was deleted successfully!" };
            }
            catch (DbUpdateException dbEx)
            {

                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";

                return new ServiceResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbErrorMessage,
                    Data = null
                };
            }
        }

        public async Task<ServiceResult> CreateMultipleAsync(CreateStudentDto[] studentDtos)
        {
            try
            {
                var studentModels = studentDtos.Select(dto => dto.ToStudentFromCreate()).ToArray();
                var newStudentsDto = await _studentRepo.CreateMultipleAsync(studentModels);
                if (newStudentsDto.Length == 0) return new ServiceResult
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "All students already exist."
                };
                else if (newStudentsDto.Length < studentModels.Length) return new ServiceResult
                {
                    StatusCode = StatusCodes.Status206PartialContent, // Partial content status code
                    Message = "Some students were created successfully.",
                    Data = newStudentsDto
                };

                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Students were created successfully", Data = newStudentsDto };
            }
            catch (DbUpdateException dbEx)
            {

                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";

                return new ServiceResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbErrorMessage,
                    Data = null
                };
            }
        }

        public async Task<ServiceResult> CreateGradesAsync(CreateGradesReportDto gradesDto)
        {
            try
            {
                _logger.LogInformation("Received Grades DTO: {@GradesDto}", gradesDto);
                var gradesList = new List<Grade>();
                foreach (var keyValuePair in gradesDto.Grades)
                {
                    string key = keyValuePair.Key;
                    CreateGradeDto[] gradesValues = keyValuePair.Value;
                    foreach (var grade in gradesValues)
                    {
                        Grade gradeModel = grade.ToGradeFromCreate(key);
                        gradesList.Add(gradeModel);
                    }
                }

                await _gradeRepo.CreateMultipleAsync(gradesList);
                _logger.LogInformation("Grades created successfully for {Count} students.", gradesList.Count);
                return new ServiceResult { StatusCode = StatusCodes.Status201Created, Message = "Grades were added successfully" };
            }
            catch (DbUpdateException dbEx)
            {

                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";

                return new ServiceResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbErrorMessage,
                    Data = null
                };
            }
        }
    }
}

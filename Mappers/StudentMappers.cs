using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Student;
using api.Models;

namespace api.Mappers
{
    public static class StudentMappers
    {
        public static Student ToStudentFromCreate(this CreateStudentDto studentDto)
        {
            return new Student
            {
                StudentId = studentDto.StudentId,
                FirstName = studentDto.FirstName.ToLower(),
                LastName = studentDto.LastName.ToLower(),
                PhoneNumber = studentDto.PrefixPhoneNumber + studentDto.PhoneNumber,
            };
        }

        public static NewStudentDto ToNewStudentDtoFromModel(this Student studentModel)
        {
            return new NewStudentDto
            {
                StudentId = studentModel.StudentId,
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                PhoneNumber = studentModel.PhoneNumber,
            };
        }

    }
}

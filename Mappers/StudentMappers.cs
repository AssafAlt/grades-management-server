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
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                PhoneNumber = studentDto.PrefixPhoneNumber + studentDto.PhoneNumber,
            };
        }

    }
}

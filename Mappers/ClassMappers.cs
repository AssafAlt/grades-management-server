using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Class;
using api.Dtos.Student;
using api.Models;

namespace api.Mappers
{
    public static class ClassMappers
    {

        public static NewClassDto ToNewClassDtoFromModel(this Class classModel, int amountOfStudents)
        {
            return new NewClassDto
            {
                ClassId = classModel.ClassId,
                ClassName = classModel.ClassName,
                GroupId = classModel.GroupId,
                AmountOfStudents = amountOfStudents,
                Students = classModel.Students?.Select(s => s.ToNewStudentDtoFromModel()).ToList() ?? new List<NewStudentDto>()


            };
        }
        public static Class ToClassFromCreate(this CreateClassDto classDto, string teacherId)
        {
            return new Class
            {
                ClassName = classDto.ClassName.ToUpper(),
                TeacherId = teacherId,
                GroupId = classDto.GroupId


            };
        }

    }
}
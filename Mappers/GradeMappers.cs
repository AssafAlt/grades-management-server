using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Grade;
using api.Models;

namespace api.Mappers
{
    public static class GradeMappers
    {
        public static Grade ToGradeFromCreate(this CreateGradeDto gradeDto, string studentId)
        {
            return new Grade
            {
                StudentId = studentId,
                GradeItemId = gradeDto.GradeItemId,
                Score = gradeDto.Score
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GradeItem;
using api.Models;

namespace api.Mappers
{
    public static class GradeItemMappers
    {
        public static GradeItem ToGradeItemFromCreate(this CreateGradeItemDto gradeItemDto)
        {
            return new GradeItem
            {
                Name = gradeItemDto.Name,
                Weight = gradeItemDto.Weight / 100m,
                ClassId = gradeItemDto.ClassId
            };
        }
    }
}
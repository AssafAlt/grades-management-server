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
        public static GradeItem ToGradeItemFromCreate(this CreateGradeItemDto gradeItemDto, int classId)
        {
            return new GradeItem
            {
                Name = gradeItemDto.Name.ToUpper(),
                Weight = gradeItemDto.Weight / 100m,
                ClassId = classId
            };
        }

        public static GradeItemDto ToGradeItemDtoFromModel(this GradeItem gradeItemModel)
        {
            return new GradeItemDto
            {
                GradeItemId = gradeItemModel.GradeItemId,
                Name = gradeItemModel.Name.ToUpper(),
                Weight = gradeItemModel.Weight

            };
        }
    }
}
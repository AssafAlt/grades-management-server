using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GradeItem;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IGradeItemRepository
    {
        public Task<GradeItemDto?> CreateAsync(GradeItem gradeItem);
        public Task<int?> DeleteAsync(int gradeItemId);
        public Task<int?> UpdateAsync(UpdateGradeItemsDto updateGradeItemsDto);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.GradeItem;
using api.Interfaces.Repository;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.repository
{
    public class GradeItemRepository : IGradeItemRepository
    {

        private readonly ApplicationDBContext _context;

        public GradeItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<GradeItemDto?> CreateAsync(GradeItem gradeItem)
        {
            // Calculate the total weight of existing grade items for the class
            var totalWeight = await _context.GradeItems
                                            .Where(g => g.ClassId == gradeItem.ClassId)
                                            .SumAsync(g => g.Weight);

            // Check if adding the new grade item would exceed the total weight limit
            if (totalWeight + gradeItem.Weight > 1)
            {
                return null;
            }

            // Add the new grade item
            await _context.GradeItems.AddAsync(gradeItem);
            await _context.SaveChangesAsync();
            return gradeItem.ToGradeItemDtoFromModel();
        }

        public async Task<int?> DeleteAsync(int gradeItemId)
        {
            var gradeItemModel = await _context.GradeItems.FirstOrDefaultAsync(x => x.GradeItemId == gradeItemId);

            if (gradeItemModel == null) return null;


            _context.GradeItems.Remove(gradeItemModel);
            await _context.SaveChangesAsync();

            return 1;

        }
        public async Task<int?> UpdateAsync(UpdateGradeItemsDto updateGradeItemsDto)
        {

            var gradeItemIds = updateGradeItemsDto.GradeItemDtos.Select(dto => dto.GradeItemId).ToList();


            var existingGradeItems = await _context.GradeItems
                .Where(g => gradeItemIds.Contains(g.GradeItemId))
                .ToListAsync();


            if (existingGradeItems.Count == 0)
            {

                return null;
            }

            decimal totalWeight = 0;
            foreach (var updateDto in updateGradeItemsDto.GradeItemDtos)
            {
                var existingGradeItem = existingGradeItems.First(g => g.GradeItemId == updateDto.GradeItemId);
                existingGradeItem.Weight = updateDto.Weight / 100m;
                totalWeight += updateDto.Weight / 100m;

            }



            if (totalWeight > 1)
            {

                return -1;
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return the number of updated GradeItems
            return updateGradeItemsDto.GradeItemDtos.Count;
        }



    }
}


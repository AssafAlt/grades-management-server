using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces.Repository;
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
        public async Task CreateAsync(GradeItem gradeItem)
        {
            // Calculate the total weight of existing grade items for the class
            var totalWeight = await _context.GradeItems
                                            .Where(g => g.ClassId == gradeItem.ClassId)
                                            .SumAsync(g => g.Weight);

            // Check if adding the new grade item would exceed the total weight limit
            if (totalWeight + gradeItem.Weight > 1)
            {
                throw new InvalidOperationException("Total weight of grade items for the class cannot exceed 1.");
            }

            // Add the new grade item
            await _context.GradeItems.AddAsync(gradeItem);
            await _context.SaveChangesAsync();
        }
    }
}
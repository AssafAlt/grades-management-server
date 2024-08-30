using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Grade;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IGradeRepository
    {
        public Task CreateAsync(Grade grade);
        public Task CreateMultipleAsync(List<Grade> grades);
        public Task<FinalGradeDto> GetFinalGradesByClassId(int classId);
        public Task<FinalGradeDto> GetGradesByClassId(int classId);

        public Task DeleteFinalGradesAsync(int classId);

        public Task SaveFinalGrade(FinalGradeDto finalGradeDto, int classId);
    }
}
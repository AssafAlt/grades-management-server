using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces.Repository
{
    public interface IGradeRepository
    {
        public Task CreateAsync(Grade grade);
    }
}
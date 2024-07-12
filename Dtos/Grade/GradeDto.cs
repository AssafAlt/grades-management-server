using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GradeItem;

namespace api.Dtos.Grade
{
    public class GradeDto
    {
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public double Score { get; set; }

    }

}
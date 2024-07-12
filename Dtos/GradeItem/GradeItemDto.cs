using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.GradeItem
{
    public class GradeItemDto
    {
        public int GradeItemId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }

    }
}
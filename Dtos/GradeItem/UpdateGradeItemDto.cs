using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.GradeItem
{
    public class UpdateGradeItemDto
    {
        [Required]
        public int GradeItemId { get; set; }
        [Required]
        public decimal Weight { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.GradeItem
{
    public class CreateGradeItemDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 100)]
        public int Weight { get; set; }
        [Required]
        public int ClassId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Grade
{
    public class CreateGradeDto
    {

        [Required]
        public string StudentId { get; set; }
        [Required]
        public int GradeItemId { get; set; }
        [Required]
        [Range(0, 100)]
        public double Score { get; set; }
    }
}
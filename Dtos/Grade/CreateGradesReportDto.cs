using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Grade
{
    public class CreateGradesReportDto
    {
        [Required]
        public Dictionary<string, CreateGradeDto[]> Grades { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Student;

namespace api.Dtos.Grade
{
    public class FinalGradeDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<StudentFinalGradeDto> Students { get; set; }
        public double ClassAverage { get; set; }
    }

}
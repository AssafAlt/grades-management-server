using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Grade;
using api.Dtos.GradeItem;

namespace api.Dtos.Student
{
    public class StudentFinalGradeDto
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public List<GradeDto> Grades { get; set; }
        public double FinalGrade { get; set; }
    }
}
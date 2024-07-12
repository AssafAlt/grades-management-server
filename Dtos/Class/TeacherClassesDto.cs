using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GradeItem;
using api.Dtos.Student;



namespace api.Dtos.Class
{
    public class TeacherClassesDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public ICollection<StudentDto> Students { get; set; }
        public ICollection<GradeItemDto> GradeItems { get; set; }
    }
}
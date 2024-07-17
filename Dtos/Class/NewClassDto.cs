using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Student;

namespace api.Dtos.Class
{
    public class NewClassDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int GroupId { get; set; }
        public int AmountOfStudents { get; set; }

        public ICollection<NewStudentDto>? Students { get; set; }
    }
}
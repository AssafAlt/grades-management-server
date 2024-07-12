using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<Class> Classes { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Students")]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Student
    {
        [Key]
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<Class> Classes { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }

}
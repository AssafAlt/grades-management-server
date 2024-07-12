using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Classes")]
    [Index(nameof(ClassName), nameof(TeacherId), nameof(GroupId), IsUnique = true)]
    public class Class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string TeacherId { get; set; }
        public AppUser Teacher { get; set; }
        public int GroupId { get; set; }


        // Navigation property for many-to-many relationship
        public ICollection<Student> Students { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<GradeItem> GradeItems { get; set; }


    }
}
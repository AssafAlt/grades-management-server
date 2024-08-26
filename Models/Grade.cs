using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Grades")]
    [Index(nameof(StudentId), nameof(GradeItemId), IsUnique = true)]
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public int GradeItemId { get; set; }
        public GradeItem GradeItem { get; set; }
        public double Score { get; set; }
    }

}
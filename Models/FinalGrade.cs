using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("FinalGrades")]
    public class FinalGrade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinalGradeId { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }

        // Single JSON column to store all relevant data
        public string Json { get; set; }

        // Navigation property for Class

    }
}

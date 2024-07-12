using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{

    [Table("GradeItems")]

    [Index(nameof(ClassId), nameof(Name), IsUnique = true)]
    public class GradeItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeItemId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }
}
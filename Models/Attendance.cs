using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Attendances")]
    public class Attendance
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AttendanceId { get; set; }
    public string StudentId { get; set; }
    public Student Student { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
}
}
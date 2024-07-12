using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class CreateAttendanceDto
    {
      [Required]
      public string StudentId { get; set; }
      [Required]
      public int ClassId { get; set; }
      [Required]
      public DateTime Date { get; set; }
      [Required]
      public bool IsPresent { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Class
{
    public class RemoveStudentRequestDto
    {
        [Required]
        public string StudentId { get; set; }
    }
}
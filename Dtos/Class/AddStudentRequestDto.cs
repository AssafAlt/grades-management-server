using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Class
{
    public class AddStudentRequestDto
    {
        [Required]
        public string StudentId;
    }
}
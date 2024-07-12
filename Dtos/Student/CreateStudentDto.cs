using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Student
{
    public class CreateStudentDto
    {
        [Required]
        [RegularExpression("^[0-9]{4,8}$", ErrorMessage = "StudentId must be a numeric string between 4 to 8 digits")]
        public string StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
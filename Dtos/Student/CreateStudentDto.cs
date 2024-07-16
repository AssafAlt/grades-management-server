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
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "StudentId must be a numeric string of 6 digits")]
        public string StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"^0\d{1,2}$", ErrorMessage = "Prefix must be numeric, start with 0, and be 2 to 3 digits long.")]
        public string PrefixPhoneNumber { get; set; }

        [RegularExpression(@"^\d{7}$", ErrorMessage = "Phone number must be exactly 7 digits.")]
        public string PhoneNumber { get; set; }
    }
}
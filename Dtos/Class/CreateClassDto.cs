using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GradeItem;

namespace api.Dtos.Class
{
    public class CreateClassDto
    {
        [Required]
        public string ClassName { get; set; }
        [Required]
        public int GroupId { get; set; }

    }
}
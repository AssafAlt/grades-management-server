using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.GradeItem
{
    public class UpdateGradeItemsDto
    {
        [Required]
        public List<UpdateGradeItemDto> GradeItemDtos { get; set; }
    }
}
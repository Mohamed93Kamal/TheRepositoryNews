using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [Display(Name = "اضافة تصنيف")]
        public string Name { get; set; }
    }
}

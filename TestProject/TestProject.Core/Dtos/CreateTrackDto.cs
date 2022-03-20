using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Dtos
{
    public class CreateTrackDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = " العنوان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = " الوقت")]
        public int TimeInMinute { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = " ملف الصوت")]
        public IFormFile TrackUrl { get; set; }
        [Display(Name = " التصنيف ")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = " المالك")]
        public string OwnerName { get; set; }
    }
}

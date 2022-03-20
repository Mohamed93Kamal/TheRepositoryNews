using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Dtos
{
   public class CreateAdvertisementDto
    {
        [Display(Name ="العنوان")]
        [Required (ErrorMessage ="هذا الحقل مطلوب")]
        public string Title { get; set; }
        [Display(Name = "الصورة")]
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public IFormFile ImgUrl { get; set; }
        [Display(Name = "الموقع الالكتروني")]
        [DataType(DataType.Url)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string WebsiteUrl { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ البداية")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ النهاية")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "السعر")]
        public float Price { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public CreateUserDto Owner { get; set; }
        [Display(Name = "صاحب الاعلان")]
        public string OwnerId { get; set; }
    }
}

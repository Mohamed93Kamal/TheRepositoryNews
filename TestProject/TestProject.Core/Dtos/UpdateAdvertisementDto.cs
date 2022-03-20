using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Dtos
{
   public class UpdateAdvertisementDto
    {
        public int Id { get; set; }
        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Title { get; set; }
        [Display(Name = "الصورة")]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImgUrl { get; set; }
        [Display(Name = "الموقع الالكتروني")]
        [Url]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "تاريخ البداية")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ النهاية")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime EndDate { get; set; }
        [Display(Name = "السعر")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public float Price { get; set; }
    }
}

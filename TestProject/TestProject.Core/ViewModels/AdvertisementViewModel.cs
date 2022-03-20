using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.ViewModels
{
   public class AdvertisementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string WebsiteUrl { get; set; }
        public float Price { get; set; }      
        public UserViewModel Owner { get; set; }
    }
}

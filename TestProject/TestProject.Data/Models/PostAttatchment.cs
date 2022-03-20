using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Data.Models
{
    public class PostAttatchment
    {
        public int Id { get; set; }
        [Required]
        public string AttachmentUrl { get; set; }
        public int PostId { get; set; }
        public Post post { get; set; }
    }
}

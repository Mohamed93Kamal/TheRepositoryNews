using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Enums;

namespace TestProject.Data.Models
{
   public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }
        public DateTime SendAt { get; set; }
        public NotificationAction Action { get; set; }
        public string ActionId { get; set; }
    }
}

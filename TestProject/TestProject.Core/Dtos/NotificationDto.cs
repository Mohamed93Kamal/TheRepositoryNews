using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Enums;

namespace TestProject.Core.Dtos
{
    public class NotificationDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public NotificationAction Action { get; set; }
        public string ActionId { get; set; }
    }
}

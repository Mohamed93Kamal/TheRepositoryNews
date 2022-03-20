﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Enums;

namespace TestProject.Data.Models
{
   public class Post : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int TimeInMinute { get; set; }
        public ContentStatus Status { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
        public List<PostAttatchment> Attatchments { get; set; }

        public Post()
        {
            Status = ContentStatus.Pending;
        }
    }
}

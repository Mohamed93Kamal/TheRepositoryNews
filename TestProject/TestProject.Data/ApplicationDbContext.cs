using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Data.Models;

namespace TestProject.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ContentChangeLog> contentChangeLogs { get; set; }
        public DbSet<Email> emails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<PostAttatchment> postAttatchments { get; set; }
        public DbSet<Track> tracks { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data.Models;
using TestProject.Web.Data;

namespace TestProject.Data
{
   public static class DataSeeder
    {
        public static IHost SeedData(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                context.SeederCategory().Wait();
                userManager.seederUser().Wait();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return webHost;
        }

        public static async Task seederUser(this UserManager<User> userManger)
        {
            if (await userManger.Users.AnyAsync())
            {
                return;
            }

            var user = new User();
            user.Email = "Developer@Gmail.com";
            user.FullName = "System Develpoer";
            user.UserName = "Developer@Gmail.com";
            user.CreatedAt = DateTime.Now;

            await userManger.CreateAsync(user, "Developer123**");
        }
        public static async Task SeederCategory(this ApplicationDbContext _db)
        {
            if (await _db.categories.AnyAsync())
            {
                return;
            }

            var categoires = new List<Category>();

            var category = new Category();
            category.Name = "Sys1";
            category.CreatedAt = DateTime.Now;

            var category1 = new Category();
            category1.Name = "Sys2";
            category1.CreatedAt = DateTime.Now;

            categoires.Add(category);
            categoires.Add(category1);

            await _db.categories.AddRangeAsync(categoires);
            await _db.SaveChangesAsync();
        }

    }
}

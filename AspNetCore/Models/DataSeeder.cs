using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Globalization;
using AspNetCore.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Models;
using AspNetCore.Services;
//Reference:
//http://stackoverflow.com/questions/34536021/seed-initial-data-in-entity-framework-7-rc-1-and-asp-net-mvc-6
namespace AspNetCore.Models
{
    public static class DataSeeder
    {
        public static async void SeedData(this IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //Caution: Clear all the tables in the database first.
                db.Database.Migrate();

                UserService userService = new UserService(db);
                UserInfo admin = new UserInfo()
                {
                    FirstName = "admin",
                    LastName = "adminuser",
                    UserName = "admin",
                    Roles = "Admin"

                };

                UserInfo getAdmin = userService.Create(admin, "admin");

                UserInfo instructor = new UserInfo()
                {
                    FirstName = "tester",
                    LastName = "testing",
                    UserName = "test",
                    Roles = "Instructor"

                };

                UserInfo getInstructor = userService.Create(instructor, "test");

               



                db.SaveChanges();


                return;
            }//End of SeedData() method




        }
    }
}





using System.Collections.Generic;
using System.Threading.Tasks;
using BugTrackerAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await roleManager.Roles.AnyAsync()) return;

            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "Project Manager" },
                new Role { Name = "Developer" }
            };

            foreach (var role in roles)
            {
                System.Console.WriteLine(role.Name);
                await roleManager.CreateAsync(role);
            }
        }
    }
}
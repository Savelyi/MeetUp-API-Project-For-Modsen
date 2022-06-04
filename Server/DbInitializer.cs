using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository;
using System.Threading.Tasks;

namespace Server
{
    public class DbInitializer
    {
        const string adminUserName= "admin";
        const string adminFullName= "adminName";
        const string password = "123456";
        const string AdminEmail = "admin@gmail.com";
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                User admin = new User { UserName = adminUserName, Email=AdminEmail, FullName = adminFullName};
                var result= await userManager.CreateAsync(admin,password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");

                }
            }

        }
    }
}

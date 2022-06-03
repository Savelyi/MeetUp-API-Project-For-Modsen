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
        const string password = "123456";
        const string AdminEmail = "Savelyi@gmail.com";
        public static async Task InitializeAsync(UserManager<User> userManager)
        {
            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                User admin = new User { UserName = adminUserName, Email=AdminEmail};
                var result= await userManager.CreateAsync(admin,password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");

                }
            }
        }
    }
}

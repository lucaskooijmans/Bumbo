using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Data;

public static class UserAndRoleSeeder
{
    public static void SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        SeedUsers(userManager);
    }
    
    public static void SeedUsers(UserManager<AppUser> userManager)
    {
        if (userManager.FindByEmailAsync("employee@example.com").Result == null)
        {
            AppUser user = new AppUser();
            user.UserName = "employee@example.com";
            user.Email = "employee@example.com";
            user.Id = "a44ff950-3f58-4c29-8cb5-faec974176d8";
            
            IdentityResult result = userManager.CreateAsync(user, "Test123!").Result;
            
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Employee").Wait();
            }
        }

        if (userManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            AppUser user = new AppUser();
            user.UserName = "admin@example.com";
            user.Email = "admin@example.com";
            user.Id = "2ff55d25-e409-4297-a212-1b7b236b8e2e";

            IdentityResult result = userManager.CreateAsync(user, "Test123!").Result;
            
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Manager").Wait();
            }
        }
    }    
}
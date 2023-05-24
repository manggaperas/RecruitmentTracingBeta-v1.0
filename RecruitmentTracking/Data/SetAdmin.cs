using Microsoft.AspNetCore.Identity;
using RecruitmentTracking.Models;

namespace RecruitmentTracking.Data;

public static class SetAdmin
{
    public static async void CreateAdminData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var adminUser = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await SetAdminAccount(adminUser);
    }
    static async Task SetAdminAccount(UserManager<User> userManager)
    {
        string adminEmail = "admin@formulatrix.com";
        string adminPassword = "Admin0!!";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new User
            {
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

}
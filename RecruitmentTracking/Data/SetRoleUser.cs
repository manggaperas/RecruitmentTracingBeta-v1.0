using Microsoft.AspNetCore.Identity;

namespace RecruitmentTracking.Data;

public static class SetRoleUser
{
    public static async void CreateRoles(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await SetRolesAsync(roleManager);

    }
    static async Task SetRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "Candidate" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}

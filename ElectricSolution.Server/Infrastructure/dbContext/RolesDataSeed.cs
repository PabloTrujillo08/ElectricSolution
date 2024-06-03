using Microsoft.AspNetCore.Identity;


public static class RolesDataSeed
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
      
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Inicializar roles
        string[] roleNames = { "Admin", "User", "Demo", "Premium" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }
    }
}
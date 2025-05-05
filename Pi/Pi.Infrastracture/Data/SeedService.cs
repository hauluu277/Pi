using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pi.Domain.Entities.Identity;

public class SeedService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

        // Tạo role nếu chưa có
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            await roleManager.CreateAsync(new IdentityRole<long>("Administrator"));
        }

        // Nếu chưa có user admin
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new Users
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "12345678");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }
        }
    }
}

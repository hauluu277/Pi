using Microsoft.AspNetCore.Identity;
using Pi.Domain.Entities.Identity;
using Pi.Infrastracture.Data;

namespace Pi.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<Users, Roles>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<PiContext>()
                .AddDefaultTokenProviders();
        }
    }
}

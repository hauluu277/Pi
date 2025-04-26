using Microsoft.AspNetCore.Identity;
using Pi.Domain.Entities.Identity;
using Pi.Infrastracture.Data;

namespace Pi.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;              // Không yêu cầu số
                options.Password.RequireUppercase = false;          // Không yêu cầu chữ hoa
                options.Password.RequireLowercase = false;          // Không yêu cầu chữ thường
                options.Password.RequireNonAlphanumeric = false;    // Không yêu cầu ký tự đặc biệt
                options.Password.RequiredLength = 8;                 // Mật khẩu ít nhất 8 ký tự
                options.User.RequireUniqueEmail = true;              // Yêu cầu email duy nhất
            });
            //.AddEntityFrameworkStores<PiContext>()
            //.AddDefaultTokenProviders();


            // có thể thêm policy hoặc role setup nếu cần
            return services;
        }
    }
}

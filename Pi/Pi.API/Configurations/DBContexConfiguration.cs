using Microsoft.EntityFrameworkCore;
using Pi.Infrastracture.Data;

namespace Pi.API.Configurations
{
    public static class DBContexConfiguration
    {
        public static void AddDBContextServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<PiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}

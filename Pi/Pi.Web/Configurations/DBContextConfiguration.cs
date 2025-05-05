using Pi.Infrastracture.Data;

namespace Pi.Web.Configurations
{
    public static class DBContextConfiguration
    {
        public static void AddDBContextService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<PiContext>(option =>
            {

            });
        }
    }
}

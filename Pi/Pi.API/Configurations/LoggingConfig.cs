using Microsoft.AspNetCore.Builder;
using Serilog;
using System.Runtime.CompilerServices;

namespace Pi.API.Configurations
{
    public static class LoggingConfig
    {
        public static void AddSerilogLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day,
                retainedFileCountLimit:7, //giữ tối đa 7 file log
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
            builder.Host.UseSerilog();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Infrastructure.Serilog
{
    internal static class ConfigureServices
    {
        public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, config) =>
            config.ReadFrom.Configuration(context.Configuration));

            return hostBuilder;
        }
    }
}

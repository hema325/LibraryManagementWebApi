using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundServices
{
    internal static class ConfigureBackgroundServices
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<RemoveExpiredRefreshTokensBackgroundService>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Services
{
    internal static class ConfigureCommonServices
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, DateTimeService>();

            return services;
        }
    }
}

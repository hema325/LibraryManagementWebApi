using Infrastructure.Multitenancy;
using Infrastructure.MultiTenancy.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MultiTenancy
{
    internal static class ConfigureMultiTenancyService
    {
        public static IServiceCollection AddMultiTenancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICurrentTenant, CurrentTenantService>();

            services.Configure<TenancySettings>(configuration.GetSection(TenancySettings.SectionName));

            return services;
        }
    }
}

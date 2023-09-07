using Infrastructure.Cors.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cors
{
    internal static class ConfigureCorsService
    {
        private const string CorsPolicyName = "CorsPolicy";
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(o =>
            {
                var corsSettings = configuration.GetSection(CorsSettings.SectionName).Get<CorsSettings>();

                o.AddPolicy(CorsPolicyName, policy =>
                {
                    policy.WithOrigins(corsSettings!.Origins);
                    policy.WithMethods(corsSettings!.Methods);
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsService(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicyName);

            return app;
        }
    }
}

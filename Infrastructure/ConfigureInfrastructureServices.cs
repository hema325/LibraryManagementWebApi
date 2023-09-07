using Infrastructure.Common.Services;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Authentication;
using Infrastructure.BackgroundServices;
using Infrastructure.FileStorage;
using Infrastructure.MultiTenancy;
using Infrastructure.Swagger;
using Serilog;
using Infrastructure.Serilog;
using Infrastructure.Cors;

namespace Infrastructure
{
    public static class ConfigureInfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommon();
            services.AddIdentity();
            services.AddAuth(configuration);
            services.AddPersistence(configuration);
            services.AddBackgroundServices();
            services.AddFileStorage();
            services.AddMultiTenancy(configuration);
            services.AddSwagger();
            services.AddCorsService(configuration);

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseSwagger(serviceProvider);
            app.UseCorsService();
            app.UseFileStorage();
            app.UseAuth();  

            return app;
        }

        public static ConfigureHostBuilder AddInfrastructure(this ConfigureHostBuilder hostBuilder)
        {
            hostBuilder.AddSerilog();

            return hostBuilder;
        }

        public static async Task<IServiceProvider> InitialiseDbAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>().InitialiseAsync();

            return serviceProvider;
        }
    }
}

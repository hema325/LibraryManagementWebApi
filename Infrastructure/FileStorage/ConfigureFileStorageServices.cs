using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.FileStorage
{
    internal static class ConfigureFileStorageServices
    {
        public static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            services.AddScoped<IFileStorage, LocalFileStorageService>();

            return services;
        }
        public static IApplicationBuilder UseFileStorage(this IApplicationBuilder app)
        {
            app.UseStaticFiles();

            return app;
        }
    }
}

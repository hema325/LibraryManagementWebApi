namespace Presentation.Middlewares
{
    public static class ConfigureMiddlewareServices
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionHandlerMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            return app;
        }
    }
}

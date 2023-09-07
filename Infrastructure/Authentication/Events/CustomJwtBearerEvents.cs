using Infrastructure.MultiTenancy.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Infrastructure.Authentication.Events
{
    internal class CustomJwtBearerEvents: JwtBearerEvents
    {
        private readonly TenancySettings _tenancySettings;
        private readonly IServiceProvider _serviceProvider;

        public CustomJwtBearerEvents(IServiceProvider serviceProvider, IOptions<TenancySettings> tenancySettings)
        {
            _serviceProvider = serviceProvider;
            _tenancySettings = tenancySettings.Value;
        }

        public override Task TokenValidated(TokenValidatedContext context)
        {
            var currentTenant = _serviceProvider.CreateScope().ServiceProvider
                .GetRequiredService<ICurrentTenant>();

            if (_tenancySettings.IsEnabled)
            {
                if (currentTenant.Id != context.Principal?.FindFirstValue("tenant"))
                    context.Fail("You are not allowed to access this tenant");
            }

            return base.TokenValidated(context);
        }
    }
}

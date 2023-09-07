using Infrastructure.Multitenancy.Constants;
using Infrastructure.Multitenancy.Models;
using Infrastructure.MultiTenancy.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Multitenancy
{
    internal class CurrentTenantService: ICurrentTenant
    {
        private readonly Tenant? _currentTenant;
        private readonly TenancySettings _tenancySettings; 

        public CurrentTenantService(IHttpContextAccessor httpContextAccessor, IOptions<TenancySettings> tenancySettings)
        {
            _tenancySettings = tenancySettings.Value;
            _currentTenant = GetCurrentTenant(httpContextAccessor.HttpContext!);
        }

        private Tenant? GetCurrentTenant(HttpContext httpContext)
        {
            if (httpContext != null && _tenancySettings.IsEnabled)
            {
                var tenantId = httpContext.Request.Headers[TenancyHeaders.Tenant].ToString();
                var currentTenant = _tenancySettings.Tenants.FirstOrDefault(t => t.Id == tenantId);

                if (currentTenant == null)
                    throw new InvalidTenantException();

                return currentTenant;
            }

            return null;
        }

        public string? Id => _currentTenant?.Id;
        public string? Name => _currentTenant?.Name;
        public string? ConnectionString => _currentTenant?.ConnectionString;
    }
}

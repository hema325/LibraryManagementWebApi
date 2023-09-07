using Infrastructure.Multitenancy.Models;

namespace Infrastructure.MultiTenancy.Settings
{
    internal class TenancySettings
    {
        public const string SectionName = "Tenancy";

        public bool IsEnabled { get; init; }
        public List<Tenant> Tenants { get; init; }
    }
}

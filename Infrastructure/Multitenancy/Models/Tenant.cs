namespace Infrastructure.Multitenancy.Models
{
    internal class Tenant
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string ConnectionString { get; init; }
    }
}

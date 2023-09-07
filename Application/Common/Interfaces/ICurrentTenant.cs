namespace Application.Common.Interfaces
{
    public interface ICurrentTenant
    {
        string? Id { get; }
        string? Name { get; }
        string? ConnectionString { get; }
    }
}

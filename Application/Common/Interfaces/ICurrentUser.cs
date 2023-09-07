namespace Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string? UserName { get; }
        int? Id { get; }
    }
}

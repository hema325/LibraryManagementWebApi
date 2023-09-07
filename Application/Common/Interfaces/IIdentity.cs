using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IIdentity
    {
        Task ChangeUserPermissionsAsync(int userId, List<string> strPermissions);
        Task<string?> GetUserNameById(int id);
        Task<bool> IsInRole(int id, Roles role);
    }
}

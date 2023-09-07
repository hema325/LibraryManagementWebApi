using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Permissions
{
    internal class PermissionRequirement: IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}

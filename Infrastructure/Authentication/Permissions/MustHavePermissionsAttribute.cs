using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Permissions
{
    public class MustHavePermissionsAttribute: AuthorizeAttribute
    {
        public MustHavePermissionsAttribute(params Domain.Enums.Permissions[] permissions)
        {
            Policy = string.Join(",", permissions);
        }
    }
}

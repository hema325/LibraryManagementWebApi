using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Permissions
{
    public class HaveRolesAttribute : AuthorizeAttribute
    {
        public HaveRolesAttribute(params Roles[] roles) 
        {
            Roles = string.Join(",", roles);
        }
    }
}

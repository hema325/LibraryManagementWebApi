using Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Permissions
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userPermissions = context.User.FindAll(CustomClaims.Permission).Select(c => c.Value);

            if (userPermissions.Any(p => p == requirement.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

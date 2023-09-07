using Application.Authentication.Common;

namespace Application.Authentication.Commands.Register
{
    public record RegisterCommand(string UserName, string Password, List<string> Roles, List<string> Permissions): IRequest<AuthResult>;
}

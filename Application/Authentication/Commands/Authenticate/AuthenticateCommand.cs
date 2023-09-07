using Application.Authentication.Common;

namespace Application.Authentication.Commands.Authenticate
{
    public record AuthenticateCommand(string UserName, string Password): IRequest<AuthResult>;
}

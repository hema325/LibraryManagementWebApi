using Application.Authentication.Common;

namespace Application.Authentication.Commands.RequestJwtToken
{
    public record RequestJwtTokenCommand(string RefreshToken): IRequest<AuthResult>;
}

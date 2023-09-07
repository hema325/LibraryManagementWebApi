using Application.Authentication.Common;

namespace Application.Authentication.Commands.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string RefreshToken): IRequest;
}

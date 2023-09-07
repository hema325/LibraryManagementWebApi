using Application.Authentication.Common;

namespace Application.Common.Interfaces
{
    public interface IAuthentication
    {
        Task<AuthResult> AuthenticateAsync(string userName, string password);
        Task<AuthResult> RegisterAsync(string userName, string password, List<string> roles, List<string> permissions);
        Task<AuthResult> RequestJwtTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}

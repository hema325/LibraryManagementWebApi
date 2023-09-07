namespace Application.Authentication.Common
{
    public class AuthResult
    {
        public int UserId { get; init; }
        public string UserName { get; init; }
        public string JwtToken { get; init; }
        public DateTime JwtTokenExpiresOn { get; init; }
        public string RefreshToken { get; init; }
        public DateTime RefreshTokenExpiresOn { get; init; }
    }
}

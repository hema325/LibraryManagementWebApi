namespace Infrastructure.Authentication.Settings
{
    internal class JwtTokenSettings
    {
        public const string SectionName = "Jwt";

        public string key { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public double ExpirationInMinutes { get; init; }

    }
}

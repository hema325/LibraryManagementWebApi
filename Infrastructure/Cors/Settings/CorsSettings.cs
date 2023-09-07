namespace Infrastructure.Cors.Settings
{
    internal class CorsSettings
    {
        public const string SectionName = "Cors";

        public string[] Origins { get; init; }
        public string[] Methods { get; init; }
    }
}

namespace Presentation.Errors
{
    public class ValidationResult
    {
        public int Status { get; init; }
        public string Message { get; init; }
        public string[] Errors { get; init; }
    }
}

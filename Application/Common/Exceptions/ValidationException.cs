using System.Net;

namespace Application.Common.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public override int Status => (int)HttpStatusCode.BadRequest;

        public string[] Errors { get; }
        public ValidationException(string[] errors) : base("One or more validations have occurred") 
        {
            Errors = errors;
        }
    }
}

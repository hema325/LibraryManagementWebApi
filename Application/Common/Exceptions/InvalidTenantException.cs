using System.Net;

namespace Application.Common.Exceptions
{
    public class InvalidTenantException : ExceptionBase
    {
        public override int Status => (int)HttpStatusCode.Unauthorized;

        public InvalidTenantException() : base("Invalid tenant") { }
    }
}

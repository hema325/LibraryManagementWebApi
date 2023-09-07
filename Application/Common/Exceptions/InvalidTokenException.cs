using System.Net;

namespace Application.Common.Exceptions
{
    public class InvalidTokenException : ExceptionBase
    {
        public override int Status => (int)HttpStatusCode.Unauthorized;

        public InvalidTokenException(): base("Invalid token")
        {
            
        }
    }
}

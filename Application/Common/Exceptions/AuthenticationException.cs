using System.Net;

namespace Application.Common.Exceptions
{
    public class AuthenticationException : ExceptionBase
    {
        public override int Status => (int)HttpStatusCode.Unauthorized;

        public AuthenticationException() : base("Faild to authenticate, please check your credentials")
        {
            
        }
    }
}

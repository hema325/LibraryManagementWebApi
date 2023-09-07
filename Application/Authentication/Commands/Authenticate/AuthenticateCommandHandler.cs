using Application.Authentication.Common;

namespace Application.Authentication.Commands.Authenticate
{
    internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthResult>
    {
        private readonly IAuthentication _auth;

        public AuthenticateCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<AuthResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _auth.AuthenticateAsync(request.UserName, request.Password);
        }
    }
}

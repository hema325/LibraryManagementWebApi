using Application.Authentication.Common;

namespace Application.Authentication.Commands.RequestJwtToken
{
    internal class RequestJwtTokenCommandHandler : IRequestHandler<RequestJwtTokenCommand, AuthResult>
    {
        private readonly IAuthentication _auth;

        public RequestJwtTokenCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<AuthResult> Handle(RequestJwtTokenCommand request, CancellationToken cancellationToken)
        {
            return await _auth.RequestJwtTokenAsync(request.RefreshToken);
        }
    }
}

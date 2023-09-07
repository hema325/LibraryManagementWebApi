using Application.Authentication.Common;

namespace Application.Authentication.Commands.RevokeRefreshToken
{
    internal class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand>
    {
        private readonly IAuthentication _auth;

        public RevokeRefreshTokenCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<Unit> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
             await _auth.RevokeRefreshTokenAsync(request.RefreshToken);

            return Unit.Value;
        }
    }
}

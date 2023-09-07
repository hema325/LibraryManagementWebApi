using Application.Authentication.Common;


namespace Application.Authentication.Commands.Register
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private readonly IAuthentication _auth;

        public RegisterCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _auth.RegisterAsync(request.UserName, request.Password, request.Roles, request.Permissions);
        }
    }
}

namespace Application.Authentication.Commands.ChangePermissions
{
    internal class ChangePermissionsCommandHandler : IRequestHandler<ChangePermissionsCommand>
    {
        private readonly IIdentity _identity;

        public ChangePermissionsCommandHandler(IIdentity identity)
        {
            _identity = identity;
        }

        public async Task<Unit> Handle(ChangePermissionsCommand request, CancellationToken cancellationToken)
        {
            await _identity.ChangeUserPermissionsAsync(request.Id, request.Permissions);

            return Unit.Value;
        }
    }
}

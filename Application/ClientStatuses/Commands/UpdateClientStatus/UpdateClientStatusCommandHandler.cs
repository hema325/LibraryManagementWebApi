namespace Application.ClientStatuses.Commands.UpdateClientStatus
{
    internal class UpdateClientStatusCommandHandler : IRequestHandler<UpdateClientStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateClientStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateClientStatusCommand request, CancellationToken cancellationToken)
        {
            var clientStatus = await _context.ClientStatuses.FindAsync(request.Id, cancellationToken);

            if (clientStatus == null)
                throw new NotFoundException(nameof(ClientStatus), request.Id);

            clientStatus.Name = request.Name;
            clientStatus.Notes = request.Notes;

            clientStatus.AddDomainEvent(new EntityUpdatedEvent(clientStatus));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

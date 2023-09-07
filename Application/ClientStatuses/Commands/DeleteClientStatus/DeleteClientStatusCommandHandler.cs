namespace Application.ClientStatuses.Commands.DeleteClientStatus
{
    internal class DeleteClientStatusCommandHandler : IRequestHandler<DeleteClientStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteClientStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteClientStatusCommand request, CancellationToken cancellationToken)
        {
            var clientStatus = await _context.ClientStatuses.FindAsync(request.Id, cancellationToken);

            if (clientStatus == null)
                throw new NotFoundException(nameof(ClientStatus), request.Id);

            clientStatus.AddDomainEvent(new EntityDeletedEvent(clientStatus));

            _context.ClientStatuses.Remove(clientStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

namespace Application.Clients.Commands.UpdateClient
{
    internal class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FindAsync(request.Id, cancellationToken);

            if (client == null)
                throw new NotFoundException(nameof(Client), request.Id);

            client.Name = request.Name;
            client.Notes = request.Notes;
            client.StatusId = request.StatusId;
            client.GenderId = request.GenderId;
            client.PhoneNumbers = request.PhoneNumbers.Select(ph => new PhoneNumber { Value = ph }).ToList();

            client.AddDomainEvent(new EntityUpdatedEvent(client));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

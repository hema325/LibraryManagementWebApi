namespace Application.ClientStatuses.Commands.CreateClientStatus
{
    internal class CreateClientStatusCommandHandler : IRequestHandler<CreateClientStatusCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateClientStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateClientStatusCommand request, CancellationToken cancellationToken)
        {
            var clientStatus = new ClientStatus
            {
                Name = request.Name,
                Notes = request.Notes
            };

            clientStatus.AddDomainEvent(new EntityCreatedEvent(clientStatus));

            _context.ClientStatuses.Add(clientStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return clientStatus.Id;
        }
    }
}

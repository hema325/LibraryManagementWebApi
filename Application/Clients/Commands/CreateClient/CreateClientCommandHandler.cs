namespace Application.Clients.Commands.CreateClient
{
    internal class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                Name = request.Name,
                Notes = request.Notes,
                GenderId = request.GenderId,
                StatusId = request.StatusId,
                PhoneNumbers = request.PhoneNumbers.Select(ph => new PhoneNumber { Value = ph }).ToList()
            };

            client.AddDomainEvent(new EntityCreatedEvent(client));

            _context.Clients.Add(client);
            await _context.SaveChangesAsync(cancellationToken);

            return client.Id;
        }
    }
}

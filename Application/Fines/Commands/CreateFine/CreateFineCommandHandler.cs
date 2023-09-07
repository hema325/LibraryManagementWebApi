namespace Application.Fines.Commands.CreateFine
{
    internal class CreateFineCommandHandler : IRequestHandler<CreateFineCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateFineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFineCommand request, CancellationToken cancellationToken)
        {
            var fine = new Fine
            {
                Amount = request.Amount,
                Notes = request.Notes,
                ClientId = request.ClientId,
                LoanId = request.LoanId
            };

            fine.AddDomainEvent(new EntityCreatedEvent(fine));

            _context.Fines.Add(fine);
            await _context.SaveChangesAsync(cancellationToken);

            return fine.Id;
        }
    }
}

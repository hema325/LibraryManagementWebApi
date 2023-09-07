namespace Application.Loans.Commands.CreateLoan
{
    internal class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateLoanCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan
            {
                Notes = request.Notes,
                BookId = request.BookId,
                ClientId = request.ClientId,
                ReturnDate = request.ReturnDate
            };

            loan.AddDomainEvent(new EntityCreatedEvent(loan));

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync(cancellationToken);

            return loan.Id;
        }
    }
}

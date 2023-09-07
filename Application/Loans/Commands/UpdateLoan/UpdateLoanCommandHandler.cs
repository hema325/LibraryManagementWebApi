namespace Application.Loans.Commands.UpdateLoan
{
    internal class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLoanCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans.FindAsync(request.Id, cancellationToken);

            if (loan == null)
                throw new NotFoundException(nameof(Loan), request.Id);

            loan.Notes = request.Notes;
            loan.ClientId = request.ClientId;
            loan.BookId = request.BookId;
            loan.ReturnDate = request.ReturnDate;

            loan.AddDomainEvent(new EntityUpdatedEvent(loan));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

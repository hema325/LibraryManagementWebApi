namespace Application.Loans.Commands.DeleteLoan
{
    internal class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteLoanCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans.FindAsync(request.Id, cancellationToken);

            if(loan == null) 
                throw new NotFoundException(nameof(Loan), request.Id);

            loan.AddDomainEvent(new EntityDeletedEvent(loan));

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

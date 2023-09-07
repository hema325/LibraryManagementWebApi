namespace Application.Fines.Commands.UpdateFine
{
    internal class UpdateFineCommandHandler : IRequestHandler<UpdateFineCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFineCommand request, CancellationToken cancellationToken)
        {
            var fine = await _context.Fines.FindAsync(request.Id, cancellationToken);

            if (fine == null)
                throw new NotFoundException(nameof(Fine), request.Id);

            fine.Amount= request.Amount;
            fine.Notes= request.Notes;
            fine.LoanId = request.LoanId;
            fine.ClientId = request.ClientId;

            fine.AddDomainEvent(new EntityUpdatedEvent(fine));
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

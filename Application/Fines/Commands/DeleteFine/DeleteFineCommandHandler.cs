namespace Application.Fines.Commands.DeleteFine
{
    internal class DeleteFineCommandHandler : IRequestHandler<DeleteFineCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFineCommand request, CancellationToken cancellationToken)
        {
            var fine = await _context.Fines.FindAsync(request.Id, cancellationToken);

            if (fine == null)
                throw new NotFoundException(nameof(Fine), request.Id);

            fine.AddDomainEvent(new EntityDeletedEvent(fine));

            _context.Fines.Remove(fine);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

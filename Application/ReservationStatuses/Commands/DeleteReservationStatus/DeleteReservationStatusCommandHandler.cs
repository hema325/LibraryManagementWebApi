namespace Application.ReservationStatuses.Commands.DeleteReservationStatus
{
    internal class DeleteReservationStatusCommandHandler : IRequestHandler<DeleteReservationStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteReservationStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteReservationStatusCommand request, CancellationToken cancellationToken)
        {
            var reservationStatus = await _context.ReservationStatuses.FindAsync(request.Id, cancellationToken);

            if (reservationStatus == null)
                throw new NotFoundException(nameof(reservationStatus), request.Id);

            reservationStatus.AddDomainEvent(new EntityDeletedEvent(reservationStatus));

            _context.ReservationStatuses.Remove(reservationStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

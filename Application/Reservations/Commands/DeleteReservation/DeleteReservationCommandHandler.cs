namespace Application.Reservations.Commands.DeleteReservation
{
    internal class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteReservationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(request.Id, cancellationToken);

            if (reservation == null)
                throw new NotFoundException(nameof(Reservation), request.Id);

            reservation.AddDomainEvent(new EntityDeletedEvent(reservation));

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

namespace Application.Reservations.Commands.UpdateReservation
{
    internal class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateReservationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(request.Id, cancellationToken);

            if (reservation == null)
                throw new NotFoundException(nameof(Reservation), request.Id);

            reservation.BookId = request.Id;
            reservation.ClientId = request.ClientId;
            reservation.StatusId = request.StatusId;
            reservation.Notes = request.Notes;

            reservation.AddDomainEvent(new EntityUpdatedEvent(reservation));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

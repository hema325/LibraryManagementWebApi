namespace Application.ReservationStatuses.Commands.UpdateReservationStatus
{
    internal class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateReservationStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            var reservationStatus = await _context.ReservationStatuses.FindAsync(request.Id, cancellationToken);

            if (reservationStatus == null)
                throw new NotFoundException(nameof(reservationStatus), request.Id);

            reservationStatus.Name = request.Name;
            reservationStatus.Notes = request.Notes;

            reservationStatus.AddDomainEvent(new EntityUpdatedEvent(reservationStatus));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

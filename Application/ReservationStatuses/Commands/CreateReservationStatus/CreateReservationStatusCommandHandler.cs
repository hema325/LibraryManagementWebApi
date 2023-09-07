namespace Application.ReservationStatuses.Commands.CreateReservationStatus
{
    internal class CreateReservationStatusCommandHandler : IRequestHandler<CreateReservationStatusCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateReservationStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            var reservationStatus = new ReservationStatus
            {
                Name = request.Name,
                Notes = request.Notes
            };

            reservationStatus.AddDomainEvent(new EntityCreatedEvent(reservationStatus));
            
            _context.ReservationStatuses.Add(reservationStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return reservationStatus.Id;
        }
    }
}

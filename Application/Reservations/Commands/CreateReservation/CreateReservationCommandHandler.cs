namespace Application.Reservations.Commands.CreateReservation
{
    internal class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateReservationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = new Reservation
            {
                ClientId = request.ClientId,
                BookId = request.BookId,
                StatusId = request.StatusId,
                Notes = request.Notes
            };

            reservation.AddDomainEvent(new EntityCreatedEvent(reservation));

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }
    }
}

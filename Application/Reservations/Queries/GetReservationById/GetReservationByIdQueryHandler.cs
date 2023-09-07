using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Queries.GetReservationById
{
    internal class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (reservation == null)
                throw new NotFoundException(nameof(Reservation), request.Id);

            return reservation;
        }
    }
}

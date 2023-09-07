
using AutoMapper.QueryableExtensions;

namespace Application.Reservations.Queries.GetReservations
{
    internal class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, PaginatedList<ReservationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReservationsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReservationDto>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservations.ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(r => r.CreatedOn)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (reservations.Data.Count == 0)
                throw new NotFoundException("reservations");

            return reservations;
        }
    }
}

using AutoMapper.QueryableExtensions;

namespace Application.ReservationStatuses.Queries.GetReservationStatuses
{
    internal class GetReservationStatusesQueryHandler : IRequestHandler<GetReservationStatusesQuery, PaginatedList<ReservationStatusDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReservationStatusesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReservationStatusDto>> Handle(GetReservationStatusesQuery request, CancellationToken cancellationToken)
        {
            var reservationStauses = await _context.ReservationStatuses.ProjectTo<ReservationStatusDto>(_mapper.ConfigurationProvider)
               .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (reservationStauses.Data.Count == 0)
                throw new NotFoundException("ReservationStatuses");

            return reservationStauses;
        }

    }
}

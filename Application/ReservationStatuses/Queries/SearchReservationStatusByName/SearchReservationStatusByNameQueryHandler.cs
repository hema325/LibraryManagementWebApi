using AutoMapper.QueryableExtensions;

namespace Application.ReservationStatuses.Queries.SearchReservationStatusByName
{
    internal class SearchReservationStatusByNameQueryHandler: IRequestHandler<SearchReservationStatusByNameQuery, PaginatedList<ReservationStatusDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchReservationStatusByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReservationStatusDto>> Handle(SearchReservationStatusByNameQuery request, CancellationToken cancellationToken)
        {
            var reservationStauses = await _context.ReservationStatuses.Where(rs => rs.Name.StartsWith(request.Name))
                .ProjectTo<ReservationStatusDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (reservationStauses.Data.Count == 0)
                throw new NotFoundException(nameof(ReservationStatus), request.Name);

            return reservationStauses;
        }
    }
}

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Queries.SearchReservationsByStatus
{
    internal class SearchReservationsByStatusQueryHandler : IRequestHandler<SearchReservationsByStatusQuery, PaginatedList<ReservationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchReservationsByStatusQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReservationDto>> Handle(SearchReservationsByStatusQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservations.Where(r => r.Status.Name.StartsWith(request.Status))
                .ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(r => r.CreatedOn)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (reservations.Data.Count == 0)
                throw new NotFoundException("reservations", request.Status);

            return reservations;
        }
    }
}

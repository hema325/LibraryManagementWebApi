using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.ReservationStatuses.Queries.GetReservationStatusById
{
    internal class GetReservationStatusByIdQueryHandler : IRequestHandler<GetReservationStatusByIdQuery, ReservationStatusDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReservationStatusByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationStatusDto> Handle(GetReservationStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var reservationStatus = await _context.ReservationStatuses.ProjectTo<ReservationStatusDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(rs => rs.Id == request.Id, cancellationToken);

            if (reservationStatus == null)
                throw new NotFoundException(nameof(ReservationStatus), request.Id);

            return reservationStatus;
        }
    }
}

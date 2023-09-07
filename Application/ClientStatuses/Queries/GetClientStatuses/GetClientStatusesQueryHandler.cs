using AutoMapper.QueryableExtensions;

namespace Application.ClientStatuses.Queries.GetClientStatuses
{
    internal class GetClientStatusesQueryHandler : IRequestHandler<GetClientStatusesQuery, PaginatedList<ClientStatusDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientStatusesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ClientStatusDto>> Handle(GetClientStatusesQuery request, CancellationToken cancellationToken)
        {
            var clientStatuses = await _context.ClientStatuses.ProjectTo<ClientStatusDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (clientStatuses.Data.Count == 0)
                throw new NotFoundException("ClientStatuses");

            return clientStatuses;
        }
    }
}

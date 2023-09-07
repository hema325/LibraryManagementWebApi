using AutoMapper.QueryableExtensions;

namespace Application.ClientStatuses.Queries.SearchClientStatusByName
{
    internal class SearchClientStatusByNameQueryHandler : IRequestHandler<SearchClientStatusByNameQuery, PaginatedList<ClientStatusDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchClientStatusByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ClientStatusDto>> Handle(SearchClientStatusByNameQuery request, CancellationToken cancellationToken)
        {
            var clientStatuses = await _context.ClientStatuses.Where(cs => cs.Name.StartsWith(request.Name))
                .ProjectTo<ClientStatusDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (clientStatuses.Data.Count == 0)
                throw new NotFoundException(nameof(ClientStatus), request.Name);

            return clientStatuses;
        }
    }
}

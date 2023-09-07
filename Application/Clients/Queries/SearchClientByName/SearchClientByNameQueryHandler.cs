using AutoMapper.QueryableExtensions;

namespace Application.Clients.Queries.SearchClientByName
{
    internal class SearchClientByNameQueryHandler : IRequestHandler<SearchClientByNameQuery, PaginatedList<ClientDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchClientByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ClientDto>> Handle(SearchClientByNameQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients.Where(c=>c.Name.StartsWith(request.Name))
                .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (clients.Data.Count == 0)
                throw new NotFoundException(nameof(Client), request.Name);

            return clients;
        }
    }
}

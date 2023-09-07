using AutoMapper.QueryableExtensions;

namespace Application.Clients.Queries.GetClients
{
    internal class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, PaginatedList<ClientDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients.ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (clients.Data.Count == 0)
                throw new NotFoundException("clients");

            return clients;
        }
    }
}

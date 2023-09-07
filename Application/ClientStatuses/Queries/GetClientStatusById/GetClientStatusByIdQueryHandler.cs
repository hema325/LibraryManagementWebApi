using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.ClientStatuses.Queries.GetClientStatusById
{
    internal class GetClientStatusByIdQueryHandler : IRequestHandler<GetClientStatusByIdQuery, ClientStatusDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientStatusByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientStatusDto> Handle(GetClientStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var clientStatus = await _context.ClientStatuses.ProjectTo<ClientStatusDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cs => cs.Id == request.Id, cancellationToken);

            if (clientStatus == null)
                throw new NotFoundException(nameof(ClientStatuses), request.Id);

            return clientStatus;
        }
    }
}

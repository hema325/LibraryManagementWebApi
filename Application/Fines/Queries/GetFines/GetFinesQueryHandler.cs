using AutoMapper.QueryableExtensions;

namespace Application.Fines.Queries.GetFines
{
    internal class GetFinesQueryHandler : IRequestHandler<GetFinesQuery, PaginatedList<FineDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FineDto>> Handle(GetFinesQuery request, CancellationToken cancellationToken)
        {
            var fines = await _context.Fines.ProjectTo<FineDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(f => f.CreatedOn)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (fines.Data.Count == 0) 
                throw new NotFoundException("fines");

            return fines;
        }
    }
}

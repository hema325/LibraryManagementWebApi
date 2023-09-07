using AutoMapper.QueryableExtensions;

namespace Application.Genders.Queries.GetGenders
{
    internal class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, PaginatedList<GenderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetGendersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<GenderDto>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            var genders = await _context.Genders.ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (genders.Data.Count == 0)
                throw new NotFoundException("genders");

            return genders;
        }
    }
}

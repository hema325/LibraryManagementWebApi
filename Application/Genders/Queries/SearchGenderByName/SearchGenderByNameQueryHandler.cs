using AutoMapper.QueryableExtensions;

namespace Application.Genders.Queries.SearchGenderByName
{
    internal class SearchGenderByNameQueryHandler : IRequestHandler<SearchGenderByNameQuery, PaginatedList<GenderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchGenderByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<GenderDto>> Handle(SearchGenderByNameQuery request, CancellationToken cancellationToken)
        {
            var genders = await _context.Genders.Where(g => g.Name.StartsWith(request.Name))
                .ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (genders.Data.Count == 0)
                throw new NotFoundException(nameof(Gender), request.Name);

            return genders;
        }
    }
}

using AutoMapper.QueryableExtensions;

namespace Application.Authors.Queries.SearchAuthorByName
{
    internal class SearchAuthorByNameQueryHandler : IRequestHandler<SearchAuthorByNameQuery, PaginatedList<AuthorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchAuthorByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AuthorDto>> Handle(SearchAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            var authors = await _context.Authors.Where(a => a.Name.StartsWith(request.Name))
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (authors.Data.Count == 0)
                throw new NotFoundException("authors", request.Name);

            return authors;
        }
    }
}

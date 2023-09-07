using AutoMapper.QueryableExtensions;

namespace Application.Books.Queries.SearchBookByName
{
    internal class SearchBookByNameQueryHandler : IRequestHandler<SearchBookByNameQuery, PaginatedList<BookDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchBookByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<BookDto>> Handle(SearchBookByNameQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.Where(b => b.Name.StartsWith(request.Name))
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (books.Data.Count == 0)
                throw new NotFoundException("books");

            return books;
        }
    }
}

using AutoMapper.QueryableExtensions;

namespace Application.Books.Queries.GetBooks
{
    internal class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginatedList<BookDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.ProjectTo<BookDto>(_mapper.ConfigurationProvider)
               .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (books.Data.Count == 0)
                throw new NotFoundException("books");

            return books;
        }
    }
}

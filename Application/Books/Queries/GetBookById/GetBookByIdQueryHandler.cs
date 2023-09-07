using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Queries.GetBookById
{
    internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (book == null)
                throw new NotFoundException(nameof(Book), request.Id);

            return book;
        }
    }
}

using AutoMapper.QueryableExtensions;

namespace Application.Authors.Queries.GetAuthors
{
    internal class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, PaginatedList<AuthorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _context.Authors.ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (authors.Data.Count == 0)
                throw new NotFoundException("authors");

            return authors;
        }
    }
}

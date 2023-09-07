using AutoMapper.QueryableExtensions;

namespace Application.Categories.Queries.SearchCategoryByName
{
    internal class SearchCategoryByNameQueryHandler : IRequestHandler<SearchCategoryByNameQuery, PaginatedList<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchCategoryByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoryDto>> Handle(SearchCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.Where(c => c.Name.StartsWith(request.Name))
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (categories.Data.Count == 0)
                throw new NotFoundException(nameof(Category), request.Name);

            return categories;
        }
    }
}

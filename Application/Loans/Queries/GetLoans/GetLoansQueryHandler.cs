using AutoMapper.QueryableExtensions;

namespace Application.Loans.Queries.GetLoans
{
    internal class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, PaginatedList<LoanDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLoansQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans.ProjectTo<LoanDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            if (loan.Data.Count == 0)
                throw new NotFoundException("loans");

            return loan;
        }
    }
}

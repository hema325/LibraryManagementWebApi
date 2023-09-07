using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Loans.Queries.GetLoanById
{
    internal class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLoanByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans.ProjectTo<LoanDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (loan == null)
                throw new NotFoundException(nameof(Loan), request.Id);

            return loan;
        }
    }
}

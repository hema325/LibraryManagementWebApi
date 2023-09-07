using AutoMapper.QueryableExtensions;

namespace Application.Payments.Queries.GetPayments
{
    internal class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, PaginatedList<PaymentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPaymentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _context.Payments.ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
               .PaginateAsync(request.PageNumber, request.PageSize);

            if (payments.Data.Count == 0)
                throw new NotFoundException("payments");

            return payments;
        }
    }
}

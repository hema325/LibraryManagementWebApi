using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Fines.Queries.GetFineById
{
    internal class GetFineByIdQueryHandler : IRequestHandler<GetFineByIdQuery, FineDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFineByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FineDto> Handle(GetFineByIdQuery request, CancellationToken cancellationToken)
        {
            var fine = await _context.Fines.ProjectTo<FineDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (fine == null)
                throw new NotFoundException(nameof(Loan), request.Id);

            return fine;
        }
    }
}

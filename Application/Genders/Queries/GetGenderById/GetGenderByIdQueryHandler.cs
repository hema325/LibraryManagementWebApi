using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Genders.Queries.GetGenderById
{
    internal class GetGenderByIdQueryHandler : IRequestHandler<GetGenderByIdQuery, GenderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetGenderByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenderDto> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken)
        {
            var gender = await _context.Genders.ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            if (gender == null)
                throw new NotFoundException(nameof(Gender), request.Id);

            return gender;
        }
    }
}

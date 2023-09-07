namespace Application.Genders.Commands.CreateGender
{
    internal class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateGenderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = new Gender
            {
                Name = request.Name
            };

            gender.AddDomainEvent(new EntityCreatedEvent(gender));

            _context.Genders.Add(gender);
            await _context.SaveChangesAsync(cancellationToken);

            return gender.Id;
        }
    }
}

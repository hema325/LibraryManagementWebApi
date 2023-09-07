namespace Application.Genders.Commands.UpdateGender
{
    internal class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateGenderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = await _context.Genders.FindAsync(request.Id, cancellationToken);

            if (gender == null)
                throw new NotFoundException(nameof(Gender), request.Id);

            gender.Name = request.Name;

            gender.AddDomainEvent(new EntityUpdatedEvent(gender));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

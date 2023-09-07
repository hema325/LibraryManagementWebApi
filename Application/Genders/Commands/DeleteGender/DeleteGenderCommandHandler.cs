namespace Application.Genders.Commands.DeleteGender
{
    internal class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGenderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = await _context.Genders.FindAsync(request.Id, cancellationToken);

            if (gender == null)
                throw new NotFoundException(nameof(Gender), request.Id);

            gender.AddDomainEvent(new EntityDeletedEvent(gender));
           
            _context.Genders.Remove(gender);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

namespace Application.Authors.Command.UpdateAuthor
{
    internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAuthorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id, cancellationToken);

            if(author == null) 
                throw new NotFoundException(nameof(Author), request.Id);

            author.Name = request.Name;
            author.Notes = request.Notes;
            author.GenderId = request.GenderId;

            author.AddDomainEvent(new EntityUpdatedEvent(author));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

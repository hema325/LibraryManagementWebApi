namespace Application.Authors.Command.DeleteAuthor
{
    internal class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAuthorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id, cancellationToken);

            if(author == null) 
                throw new NotFoundException(nameof(Author), request.Id);

            author.AddDomainEvent(new EntityDeletedEvent(author));

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

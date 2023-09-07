namespace Application.Books.Commands.DeleteBook
{
    internal class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id, cancellationToken);

            if (book == null)
                throw new NotFoundException(nameof(Book), request.Id);

            book.AddDomainEvent(new EntityDeletedEvent(book));

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

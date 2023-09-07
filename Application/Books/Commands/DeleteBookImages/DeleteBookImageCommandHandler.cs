namespace Application.Books.Commands.DeleteBookImages
{
    internal class DeleteBookImageCommandHandler : IRequestHandler<DeleteBookImageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public DeleteBookImageCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(DeleteBookImageCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.BookId, cancellationToken);

            if (book == null)
                throw new NotFoundException(nameof(Book), request.BookId);

            book.Images = book.Images.ExceptBy(request.ImagesPath, i => i.Path).ToList();

            await _context.SaveChangesAsync(cancellationToken);

            //if savechanges failed then this command won't be executed
            request.ImagesPath.ForEach(p => _fileStorage.Remove(p));

            return Unit.Value;
        }
    }
}

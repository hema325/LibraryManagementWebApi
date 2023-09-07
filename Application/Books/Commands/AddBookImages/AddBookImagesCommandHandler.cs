namespace Application.Books.Commands.AddBookImages
{
    internal class AddBookImagesCommandHandler : IRequestHandler<AddBookImagesCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public AddBookImagesCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(AddBookImagesCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id, cancellationToken);

            if (book == null) 
                throw new NotFoundException(nameof(Book), request.Id);

            var paths = await Task.WhenAll(request.Images.Select(i => _fileStorage.UploadAsync(i, cancellationToken)));

            if (book.Images == null)
                book.Images = new List<Image>();

            book.Images.AddRange(paths.Select(p => new Image { Path = p }).ToList());

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                foreach (var path in paths)
                    _fileStorage.Remove(path);

                throw;
            }

            return Unit.Value;
        }
    }
}

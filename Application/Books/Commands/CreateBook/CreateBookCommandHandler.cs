using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.CreateBook
{
    internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public CreateBookCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var authors = await _context.Authors.Where(a => request.AuthorsIds.Any(i => i == a.Id)).ToListAsync(cancellationToken);

            var book = new Book
            {
                Name = request.Name,
                OwnedQuantity = request.OwnedQuantity,
                ReleasedAt = request.ReleasedAt,
                Notes = request.Notes,
                CategoryId = request.CategoryId,
                Authors = authors
            };

            book.AddDomainEvent(new EntityCreatedEvent(book));

            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.UpdateBook
{
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (book == null)
                throw new NotFoundException(nameof(Book), request.Id);

            var authors = await _context.Authors.Where(a => request.AuthorsIds.Any(i => i == a.Id)).ToListAsync(cancellationToken);

            book.Name = request.Name;
            book.OwnedQuantity = request.OwnedQuantity;
            book.ReleasedAt = request.ReleasedAt;
            book.Notes = request.Notes;
            book.CategoryId = request.CategoryId;
            book.Authors = authors;

            book.AddDomainEvent(new EntityUpdatedEvent(book));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

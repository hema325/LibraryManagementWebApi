
using Domain.Common.Events;

namespace Application.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id, cancellationToken);

            if (category == null)
                throw new NotFoundException(nameof(Category), request.Id);

            category.Name = request.Name;
            category.Notes = request.Notes;

            category.AddDomainEvent(new EntityDeletedEvent(category));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

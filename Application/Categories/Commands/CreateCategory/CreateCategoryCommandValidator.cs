using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (n, ct) => !await context.Categories.AnyAsync(c => c.Name == n, ct)).WithMessage("'{PropertyName}' already exists.");
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator: AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            RuleFor(cmd => cmd.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (cmd, n, ct) => !await context.Categories.AnyAsync(c => c.Name == n && c.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");
        }
    }
}

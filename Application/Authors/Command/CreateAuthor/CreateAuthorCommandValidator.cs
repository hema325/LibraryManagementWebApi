using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommandValidator: AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (n, ct) => !await context.Authors.AnyAsync(a => a.Name == n, ct)).WithMessage("'{PropertyName}' already exists.");

            RuleFor(c => c.GenderId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Genders.AnyAsync(g => g.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");
        }
    }
}

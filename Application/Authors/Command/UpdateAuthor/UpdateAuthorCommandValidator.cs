using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator(IApplicationDbContext context)
        {
            RuleFor(cmd => cmd.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (cmd, n, ct) => !await context.Authors.AnyAsync(a => a.Name == n && a.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");

            RuleFor(c => c.GenderId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MustAsync(async (i, ct) => await context.Genders.AnyAsync(g => g.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");
        }
    }
}

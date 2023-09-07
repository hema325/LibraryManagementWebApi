using Microsoft.EntityFrameworkCore;

namespace Application.Genders.Commands.UpdateGender
{
    public class UpdateGenderCommandValidator: AbstractValidator<UpdateGenderCommand>
    {
        public UpdateGenderCommandValidator(IApplicationDbContext context)
        {
            RuleFor(cmd => cmd.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (cmd, n, ct) => !await context.Genders.AnyAsync(g => g.Name == n && g.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");
        }
    }
}

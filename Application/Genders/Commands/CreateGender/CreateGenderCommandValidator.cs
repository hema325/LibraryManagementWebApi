using Microsoft.EntityFrameworkCore;

namespace Application.Genders.Commands.CreateGender
{
    public class CreateGenderCommandValidator: AbstractValidator<CreateGenderCommand>
    {
        public CreateGenderCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (n, ct) => !await context.Genders.AnyAsync(g => g.Name == n, ct)).WithMessage("'{PropertyName}' already exists.");
        }
    }
}

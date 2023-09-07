using Microsoft.EntityFrameworkCore;

namespace Application.ClientStatuses.Commands.CreateClientStatus
{
    public class CreateClientStatusCommandValidator: AbstractValidator<CreateClientStatusCommand>
    {
        public CreateClientStatusCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (n, ct) => !await context.ClientStatuses.AnyAsync(cs => cs.Name == n, ct)).WithMessage("'{PropertyName}' already exists.");
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Application.ClientStatuses.Commands.UpdateClientStatus
{
    public class UpdateClientStatusCommandValidator: AbstractValidator<UpdateClientStatusCommand>
    {
        public UpdateClientStatusCommandValidator(IApplicationDbContext context)
        {
            RuleFor(cmd => cmd.Name)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(256)
               .MustAsync(async (cmd, n, ct) => !await context.ClientStatuses.AnyAsync(cs => cs.Name == n && cs.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");

        }
    }
}

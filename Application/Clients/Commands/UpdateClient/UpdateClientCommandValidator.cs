using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidator: AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(256)
               .MustAsync(async (cmd, n, ct) => !await context.Clients.AnyAsync(c => c.Name == n && c.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");

            RuleFor(c => c.PhoneNumbers)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ForEach(builder =>
                {
                    builder.NotEmpty();
                    builder.MaximumLength(256);
                    builder.Must(ph => ph.All(n => char.IsDigit(n))).WithMessage("'{PropertyName}' is invalid");
                });

            RuleFor(c => c.GenderId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Genders.AnyAsync(g => g.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");

            RuleFor(c => c.StatusId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MustAsync(async (i, ct) => await context.ClientStatuses.AnyAsync(cs => cs.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");
        }
    }
}

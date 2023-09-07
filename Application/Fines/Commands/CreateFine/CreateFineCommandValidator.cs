using Microsoft.EntityFrameworkCore;

namespace Application.Fines.Commands.CreateFine
{
    public class CreateFineCommandValidator: AbstractValidator<CreateFineCommand>
    {
        public CreateFineCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .PrecisionScale(9, 2, false);

            RuleFor(c => c.LoanId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Loans.AnyAsync(l => l.Id == i, ct)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.ClientId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Clients.AnyAsync(clt => clt.Id == i, ct)).WithMessage("{PropertyName} is not valid");
        }
    }
}

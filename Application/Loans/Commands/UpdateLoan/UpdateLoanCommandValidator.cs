using Microsoft.EntityFrameworkCore;

namespace Application.Loans.Commands.UpdateLoan
{
    public class UpdateLoanCommandValidator: AbstractValidator<UpdateLoanCommand>
    {
        public UpdateLoanCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.BookId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Books.AnyAsync(b => b.Id == i, ct)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.ClientId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Clients.AnyAsync(clt => clt.Id == i, ct)).WithMessage("{PropertyName} is not valid");
        }
    }
}

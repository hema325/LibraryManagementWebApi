using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommandValidator: AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.ClientId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MustAsync(async (i, ct) => await context.Clients.AnyAsync(c => c.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");

            RuleFor(c => c.BookId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MustAsync(async (i, ct) => await context.Books.AnyAsync(b => b.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");

            RuleFor(c => c.StatusId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MustAsync(async (i, ct) => await context.ReservationStatuses.AnyAsync(s => s.Id == i, ct)).WithMessage("'{PropertyName}' is invalid");

        }
    }
}

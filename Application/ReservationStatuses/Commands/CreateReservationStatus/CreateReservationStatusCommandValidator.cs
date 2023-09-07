using Microsoft.EntityFrameworkCore;

namespace Application.ReservationStatuses.Commands.CreateReservationStatus
{
    public class CreateReservationStatusCommandValidator: AbstractValidator<CreateReservationStatusCommand>
    {
        public CreateReservationStatusCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (n, ct) => !await context.ReservationStatuses.AnyAsync(rs => rs.Name == n, ct)).WithMessage("'{PropertyName}' already exists.");
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Application.ReservationStatuses.Commands.UpdateReservationStatus
{
    public class UpdateReservationStatusCommandValidator: AbstractValidator<UpdateReservationStatusCommand>
    {
        public UpdateReservationStatusCommandValidator(IApplicationDbContext context)
        {
            RuleFor(cmd => cmd.Name)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(256)
               .MustAsync(async (cmd, n, ct) => !await context.ReservationStatuses.AnyAsync(rs => rs.Name == n && rs.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");

        }
    }
}

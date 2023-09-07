namespace Application.ReservationStatuses.Commands.CreateReservationStatus
{
    public record CreateReservationStatusCommand(string Name, string Notes): IRequest<int>;
}

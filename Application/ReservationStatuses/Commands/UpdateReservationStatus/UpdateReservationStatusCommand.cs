namespace Application.ReservationStatuses.Commands.UpdateReservationStatus
{
    public record UpdateReservationStatusCommand(int Id, string Name, string Notes): IRequest;
}

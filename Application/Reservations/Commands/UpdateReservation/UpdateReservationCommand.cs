namespace Application.Reservations.Commands.UpdateReservation
{
    public record UpdateReservationCommand(int Id, string Notes, int BookId, int ClientId, int StatusId): IRequest;
}

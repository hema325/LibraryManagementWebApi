namespace Application.Reservations.Commands.CreateReservation
{
    public record CreateReservationCommand(string Notes, int BookId, int ClientId, int StatusId): IRequest<int>;
}

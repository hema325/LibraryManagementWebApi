namespace Application.Reservations.Queries.GetReservationById
{
    public record GetReservationByIdQuery(int Id): IRequest<ReservationDto>;
}

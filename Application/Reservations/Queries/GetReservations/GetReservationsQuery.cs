namespace Application.Reservations.Queries.GetReservations
{
    public record GetReservationsQuery(int PageNumber, int PageSize): IRequest<PaginatedList<ReservationDto>>;
}

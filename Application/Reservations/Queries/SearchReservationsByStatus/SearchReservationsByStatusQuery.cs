namespace Application.Reservations.Queries.SearchReservationsByStatus
{
    public record SearchReservationsByStatusQuery(string Status, int PageNumber, int PageSize): IRequest<PaginatedList<ReservationDto>>;
}

namespace Application.ReservationStatuses.Queries.SearchReservationStatusByName
{
    public record SearchReservationStatusByNameQuery(string Name, int PageNumber, int PageSize): IRequest<PaginatedList<ReservationStatusDto>>;
}

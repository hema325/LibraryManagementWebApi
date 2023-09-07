namespace Application.ReservationStatuses.Queries.GetReservationStatuses
{
    public record GetReservationStatusesQuery(int PageNumber, int PageSize): IRequest<PaginatedList<ReservationStatusDto>>;
}

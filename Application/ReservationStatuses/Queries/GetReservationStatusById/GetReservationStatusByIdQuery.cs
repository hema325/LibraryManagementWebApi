namespace Application.ReservationStatuses.Queries.GetReservationStatusById
{
    public record GetReservationStatusByIdQuery(int Id): IRequest<ReservationStatusDto>;
}

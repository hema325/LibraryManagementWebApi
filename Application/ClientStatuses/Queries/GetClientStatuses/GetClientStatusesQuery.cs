namespace Application.ClientStatuses.Queries.GetClientStatuses
{
    public record GetClientStatusesQuery(int PageNumber, int PageSize): IRequest<PaginatedList<ClientStatusDto>>;
}

namespace Application.ClientStatuses.Queries.SearchClientStatusByName
{
    public record SearchClientStatusByNameQuery(string Name, int PageNumber, int PageSize): IRequest<PaginatedList<ClientStatusDto>>;
}

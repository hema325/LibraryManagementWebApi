namespace Application.Clients.Queries.SearchClientByName
{
    public record SearchClientByNameQuery(string Name, int PageNumber, int PageSize): IRequest<PaginatedList<ClientDto>>;
}

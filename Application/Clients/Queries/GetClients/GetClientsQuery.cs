namespace Application.Clients.Queries.GetClients
{
    public record GetClientsQuery(int PageNumber, int PageSize): IRequest<PaginatedList<ClientDto>>;
}

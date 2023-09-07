namespace Application.Clients.Queries.GetClientById
{
    public record GetClientByIdQuery(int Id): IRequest<ClientDto>;
}

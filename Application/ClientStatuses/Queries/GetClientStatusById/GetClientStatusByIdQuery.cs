namespace Application.ClientStatuses.Queries.GetClientStatusById
{
     public record GetClientStatusByIdQuery(int Id): IRequest<ClientStatusDto>;
}

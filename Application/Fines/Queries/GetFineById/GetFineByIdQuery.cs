namespace Application.Fines.Queries.GetFineById
{
    public record GetFineByIdQuery(int Id): IRequest<FineDto>;
}

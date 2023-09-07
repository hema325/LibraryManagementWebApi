namespace Application.Fines.Queries.GetFines
{
    public record GetFinesQuery(int PageNumber, int PageSize): IRequest<PaginatedList<FineDto>>;
}

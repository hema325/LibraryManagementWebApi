namespace Application.Genders.Queries.GetGenders
{
    public record GetGendersQuery(int PageNumber, int PageSize): IRequest<PaginatedList<GenderDto>>;
}

namespace Application.Genders.Queries.SearchGenderByName
{
    public record SearchGenderByNameQuery(string Name, int PageNumber,int PageSize): IRequest<PaginatedList<GenderDto>>;
}

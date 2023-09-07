namespace Application.Categories.Queries.SearchCategoryByName
{
    public record SearchCategoryByNameQuery(string Name, int PageNumber,int PageSize): IRequest<PaginatedList<CategoryDto>>;
}

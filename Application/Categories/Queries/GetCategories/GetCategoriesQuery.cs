namespace Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery(int PageNumber, int PageSize): IRequest<PaginatedList<CategoryDto>>;
}

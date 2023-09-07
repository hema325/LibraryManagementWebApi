namespace Application.Categories.Queries.GetById
{
    public record GetCategoryByIdQuery(int Id): IRequest<CategoryDto>;
}

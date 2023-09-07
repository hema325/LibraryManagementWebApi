namespace Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, string Notes): IRequest<int>;
}

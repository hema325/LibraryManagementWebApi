namespace Application.Categories.Queries
{
    public class CategoryDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Notes { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Category, CategoryDto>();
            }
        }
    }
}

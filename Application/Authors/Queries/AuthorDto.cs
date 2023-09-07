using Application.Genders.Queries;

namespace Application.Authors.Queries
{
    public class AuthorDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Notes { get; init; }
        public GenderDto Gender { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Author, AuthorDto>();
            }
        }
    }
}

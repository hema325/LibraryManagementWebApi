namespace Application.Genders.Queries
{
    public class GenderDto
    {
        public int Id { get; init; }
        public string Name { get; init; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Gender, GenderDto>();
            }
        }
    }
}

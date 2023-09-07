namespace Application.ClientStatuses.Queries
{
    public class ClientStatusDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Notes { get; init; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<ClientStatus, ClientStatusDto>();
            }
        }
    }
}

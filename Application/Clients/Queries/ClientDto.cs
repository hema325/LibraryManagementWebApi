using Application.ClientStatuses.Queries;
using Application.Genders.Queries;

namespace Application.Clients.Queries
{
    public class ClientDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Notes { get; init; }
        public List<string> PhoneNumbers { get; init; }
        public ClientStatusDto Status { get; set; }
        public GenderDto Gender { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Client, ClientDto>()
                    .ForMember(dto => dto.PhoneNumbers, o => o.MapFrom(c => c.PhoneNumbers.Select(ph => ph.Value)));
            }
        }
    }
}

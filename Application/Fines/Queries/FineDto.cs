using Application.Clients.Queries;
using Application.Loans.Queries;
using Application.Payments.Queries;

namespace Application.Fines.Queries
{
    public class FineDto
    {
        public int Id { get; init; }

        public decimal Amount { get; init; }
        public string? Notes { get; init; }
        public bool IsPaid { get; init; }
        public DateTime CreatedOn { get; init; }

        public ClientDto Client { get; init; }
        public LoanDto Loan { get; init; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Fine, FineDto>()
                    .ForMember(dto => dto.IsPaid, o => o.MapFrom(f => f.Payment != null));
            }
        }
    }
}

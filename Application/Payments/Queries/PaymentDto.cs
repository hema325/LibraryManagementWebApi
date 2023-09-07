using Application.Clients.Queries;
using Application.Fines.Queries;

namespace Application.Payments.Queries
{
    public class PaymentDto
    {
        public int Id { get; init; }
        public decimal Amount { get; init; }
        public string? Notes { get; init; }
        public DateTime CreatedOn { get; init; }

        public ClientDto Client { get; init; }
        public FineDto Fine { get; init; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Payment, PaymentDto>();
            }
        }
    }
}

using Application.Books.Queries;
using Application.Clients.Queries;

namespace Application.Loans.Queries
{
    public class LoanDto
    {
        public int Id { get; init; }
        public DateTime ReturnDate { get; init; }
        public DateTime CreatedOn { get; init; }
        public BookDto Book { get; init; }
        public ClientDto Client { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Loan, LoanDto>();
            }
        }
    }
}

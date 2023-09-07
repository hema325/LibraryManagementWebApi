using Application.Books.Queries;
using Application.Clients.Queries;
using Application.ReservationStatuses.Queries;

namespace Application.Reservations.Queries
{
    public class ReservationDto
    {
        public int Id { get; init; }
        public DateTime CreatedOn { get; init; }
        public string? Notes { get; init; }

        public BookDto Book { get; init; }
        public ClientDto Client { get; init; }
        public ReservationStatusDto Status { get; init; }


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Reservation, ReservationDto>();
            }
        }
    }
}

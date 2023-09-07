namespace Application.ReservationStatuses.Queries
{
    public class ReservationStatusDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Notes { get; init; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<ReservationStatus, ReservationStatusDto>();
            }
        }
    }
}

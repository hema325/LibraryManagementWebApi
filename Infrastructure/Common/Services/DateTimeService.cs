namespace Infrastructure.Common.Services
{
    internal class DateTimeService: IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}

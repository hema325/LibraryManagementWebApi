using Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Extensions
{
    internal static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IPublisher publisher, DbContext context)
        {
            var entries = context.ChangeTracker.Entries<EntityBase>().Where(e => e.Entity.DomainEvents.Any()).ToList();
            var domainEvents = entries.SelectMany(e => e.Entity.DomainEvents);

            await Task.WhenAll(domainEvents.Select(de => publisher.Publish(de)));
            entries.ForEach(e => e.Entity.ClearDomainEvents());
        }
    }
}

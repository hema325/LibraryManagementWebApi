using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Contracts
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        #region domainEvents
        private readonly List<EventBase> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<EventBase> DomainEvents 
            => _domainEvents;

        public void AddDomainEvent(EventBase domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
        #endregion
    }
}

namespace Domain.Common.Events
{
    public class EntityCreatedEvent: EventBase
    {
        public EntityBase Entity { get; set; }

        public EntityCreatedEvent(EntityBase entity)
        {
            Entity = entity;
        }
    }
}

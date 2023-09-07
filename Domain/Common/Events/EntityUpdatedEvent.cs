namespace Domain.Common.Events
{
    public class EntityUpdatedEvent: EventBase
    {
        public EntityBase Entity { get; set; }

        public EntityUpdatedEvent(EntityBase entity)
        {
            Entity = entity;
        }
    }
}

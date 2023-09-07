namespace Domain.Common.Events
{
    public class EntityDeletedEvent: EventBase
    {
        public EntityBase Entity { get; set; }

        public EntityDeletedEvent(EntityBase entity)
        {
            Entity = entity;
        }
    }
}

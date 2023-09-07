﻿namespace Domain.Common.Events
{
    public class EntityUpdatedEvent: EventBase
    {
        public EntityBase Entity { get; }

        public EntityUpdatedEvent(EntityBase entity)
        {
            Entity = entity;
        }
    }
}

﻿namespace Domain.Common.Events
{
    public class EntityDeletedEvent: EventBase
    {
        public EntityBase Entity { get; }

        public EntityDeletedEvent(EntityBase entity)
        {
            Entity = entity;
        }
    }
}

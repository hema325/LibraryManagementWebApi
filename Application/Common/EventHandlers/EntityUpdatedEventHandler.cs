using Domain.Common.Events;
using Microsoft.Extensions.Logging;

namespace Application.Common.EventHandlers
{
    internal class EntityUpdatedEventHandler: INotificationHandler<EntityUpdatedEvent>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;
        private readonly ILogger<EntityUpdatedEventHandler> _logger;

        public EntityUpdatedEventHandler(ICurrentUser currentUser, IDateTime dateTime, ILogger<EntityUpdatedEventHandler> logger)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {id} is updated by user {userName} with id {id} at {dateTime}",
                notification.Entity.GetType().Name,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}

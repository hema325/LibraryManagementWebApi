using Domain.Common.Events;
using Microsoft.Extensions.Logging;

namespace Application.Common.EventHandlers
{
    internal class EntityCreatedEventHandler : INotificationHandler<EntityCreatedEvent>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;
        private readonly ILogger<EntityCreatedEventHandler> _logger;

        public EntityCreatedEventHandler(ICurrentUser currentUser, IDateTime dateTime, ILogger<EntityCreatedEventHandler> logger)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {id} is created by user {userName} with id {id} at {dateTime}",
                notification.Entity.GetType().Name,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}

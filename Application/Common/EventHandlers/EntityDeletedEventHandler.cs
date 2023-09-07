using Domain.Common.Events;
using Microsoft.Extensions.Logging;

namespace Application.Common.EventHandlers
{
    internal class EntityDeletedEventHandler: INotificationHandler<EntityDeletedEvent>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;
        private readonly ILogger<EntityDeletedEventHandler> _logger;

        public EntityDeletedEventHandler(ICurrentUser currentUser, IDateTime dateTime, ILogger<EntityDeletedEventHandler> logger)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {id} is deleted by user {userName} with id {id} at {dateTime}",
                notification.Entity.GetType().Name,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}

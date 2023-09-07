using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    internal class LogginBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<LogginBehavior<TRequest>> _logger;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;

        public LogginBehavior(ILogger<LogginBehavior<TRequest>> logger, IDateTime dateTime, ICurrentUser currentUser)
        {
            _logger = logger;
            _dateTime = dateTime;
            _currentUser = currentUser;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request {requestName} has been made by user {userName} with id {userId} at {dateTime}",
                request.GetType().Name,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}


namespace Application.Common.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request)));
                var errors = validationResult.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();

                if (errors.Any())
                    throw new Exceptions.ValidationException(errors);
            }

            return await next();
        }

    }
}
